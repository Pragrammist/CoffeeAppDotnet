using Mapster;
using EFCore;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Common
{
    public abstract class DbServiceBase<TDto, TEntity> : ServiceBase  where TEntity : EntityBase
    {
        public DbServiceBase(IRepository dbRepository) : base(dbRepository)
        {
            
        }


        public IQueryable<TEntity> GetItems() => _dbRepository.Set<TEntity>();

        public IQueryable<TDto> GetEntityItems() =>
            _dbRepository.Set<TEntity>().ProjectToType<TDto>();

        public IQueryable<TPRojectType> GetItems<TPRojectType>() =>
            _dbRepository.Set<TEntity>().ProjectToType<TPRojectType>();




        public async Task<TDto> GetByIdAsync(int id, IQueryable<TEntity> items) => 
            (await items.FirstAsync(i => i.Id == id))
            .Adapt<TDto>();

        public async Task<TDto> GetByIdAsync(int id) => 
            (await GetByIdEntityAsync(id))
            .Adapt<TDto>();
        public async Task<TEntity> GetByIdEntityAsync(int id) => await _dbRepository.GetByIdAsync<TEntity>(id);

        public async Task<TDto> Create<TCreateDto>(TCreateDto item, bool withSave = true)
        {

            var entity = item?.Adapt<TEntity>() ?? throw new NullReferenceException("Item haven't mapped. Map result is null");

            return await Create(entity, withSave);
        }

        public async Task<TDto> Create(TEntity item, bool withSave = true)
        {
            await _dbRepository.AddAsync(item);
            if (withSave)
            {
                await _dbRepository.Context.SaveChangesAsync();
            }

            return item.Adapt<TDto>();
        }

        public async Task<TDto> Edit<TEditDto>(TEditDto item, bool withSave = true)
        {
            var entity = item?.Adapt<TEntity>() ?? throw new NullReferenceException("Item haven't mapped. Map result is null");

            
            return await Edit(entity, withSave);
        }
        
        public async Task<TDto> Edit(TEntity item, bool withSave = true)
        {
            _dbRepository.Update(item);
            if (withSave)
            {
                await _dbRepository.Context.SaveChangesAsync();
            }

            return item.Adapt<TDto>();
        }

        public async Task Delete(TDto dto, bool withSave = true)
        {
            var entity = dto?.Adapt<TEntity>() ?? throw new NullReferenceException("Item haven't mapped. Map result is null");

            await Delete(entity, withSave);
        }

        public async Task Delete(TEntity entity, bool withSave = true)
        {
            _dbRepository.Delete(entity);
            if (withSave)
            {
                await _dbRepository.Context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id, bool withSave = true)
        {
            var entity = await _dbRepository.GetByIdAsync<TEntity>(id);

            await Delete(entity, withSave);
        }
    }
}