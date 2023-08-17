using EFCore.Models;
using EFCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions;

namespace EFCore
{
    public interface IRepository
    {
        DbContext Context { get; }
        Task<TEntity> GetByIdAsync<TEntity>(int Id) where TEntity : EntityBase;
        void Update(EntityBase entity);
        Task<int> AddAsync(EntityBase entity);
        void Delete(EntityBase entity);

        Task Delete(int id);
        IQueryable<TEntity> GetItems<TEntity>() where TEntity : EntityBase;
    }

    public class RepositoryImpl : IRepository
    {
        public DbContext Context {  get; private set; }  

        public RepositoryImpl(DbContext context)
        {
            Context = context;
        }
        public async Task<TEntity> GetByIdAsync<TEntity>(int id) 
            where TEntity : EntityBase
        {
            var entity = await Context.FindAsync<TEntity>(id) ?? throw new DataNotValidException($"id  invalid {id}");
            return entity;
        }

        
        public IQueryable<TEntity> GetItems<TEntity>()
            where TEntity : EntityBase 
            => Context.Set<TEntity>()
            .Where(i => !i.IsDeleted);

        public void Update(EntityBase entity)
        {
            entity.LastUpdatedDate = DateTime.UtcNow;
            Context.Entry(entity).State = EntityState.Modified;
            Context.Update(entity);
        }
        public async Task<int> AddAsync(EntityBase entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            await Context.AddAsync(entity);
            return entity.Id;
        }
        public void Delete(EntityBase entity)
        {
            entity.DeletedDate = DateTime.UtcNow;
            entity.IsDeleted = true;
            Context.Entry(entity).State = EntityState.Modified;
            Context.Update(entity);
        }

        public async Task Delete(int id)
        {
            var user = await GetByIdAsync<EntityBase>(id);
            Delete(user);
        }
    }
}