using Mapster;
using EFCore;
using EFCore.Models;

namespace Services.Common
{
    public abstract class DbServiceBase<TDto, TEntity> : ServiceBase  where TEntity : EntityBase
    {
        public DbServiceBase(IRepository dbRepository) : base(dbRepository)
        {
            
        }

        public async Task<TDto> Create(TDto item, bool withSave = true)
        {

            var entity = item?.Adapt<TEntity>() ?? throw new NullReferenceException("Item haven't mapped. Map result is null");

            await _dbRepository.AddAsync(entity);
            if (withSave)
            {
                await _dbRepository.Context.SaveChangesAsync();
            }

            return entity.Adapt<TDto>();
        }

        public async Task<TDto> Edit(TDto item, bool withSave = true)
        {
            var entity = item?.Adapt<TEntity>() ?? throw new NullReferenceException("Item haven't mapped. Map result is null");

            _dbRepository.Update(entity);
            if (withSave)
            {
                await _dbRepository.Context.SaveChangesAsync();
            }

            return _dbRepository.Adapt<TDto>();
        }
        public async Task Delete(TDto dto, bool withSave = true)
        {
            var entity = dto?.Adapt<TEntity>() ?? throw new NullReferenceException("Item haven't mapped. Map result is null");

            _dbRepository.Delete(entity);
            if (withSave)
            {
                await _dbRepository.Context.SaveChangesAsync();
            }
        }
    }
}