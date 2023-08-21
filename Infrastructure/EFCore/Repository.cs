using EFCore.Models;
using EFCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions;

namespace EFCore
{
    public interface IRepository
    {
        OverallDbContext Context { get; }
        Task<TEntity> GetByIdAsync<TEntity>(int Id) where TEntity : EntityBase;
        void Update(EntityBase entity);
        Task<int> AddAsync(EntityBase entity);
        void Delete(EntityBase entity);

        Task Delete<TEntity>(int id) where TEntity : EntityBase; 
        IQueryable<TEntity> Set<TEntity>() where TEntity : EntityBase;
    }

    public class RepositoryImpl : IRepository
    {
        public OverallDbContext Context {  get; private set; }  

        public RepositoryImpl(OverallDbContext context)
        {
            Context = context;
        }
        public async Task<TEntity> GetByIdAsync<TEntity>(int id) 
            where TEntity : EntityBase
        {
            var entity = await Context.FindAsync<TEntity>(id) ?? throw new NotFoundException($"id  invalid {id}", 405);
            return entity;
        }

        
        public IQueryable<TEntity> Set<TEntity>()
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

        public async Task Delete<TEntity>(int id) where TEntity : EntityBase
        {
            var user = await GetByIdAsync<TEntity>(id);
            Delete(user);
        }
    }
}