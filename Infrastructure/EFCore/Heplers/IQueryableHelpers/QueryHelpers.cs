using Domain.Exceptions;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Heplers.IQueryableHelpers
{
    public static class QueryHelpers
    {
        public static async Task<TEntity> GetById<TEntity>(this IQueryable<TEntity> entities, int id) where TEntity : EntityBase =>
        await entities.FirstOrDefaultAsync(e => e.Id == id) ?? 
            throw new NotFoundException($"{id} of {nameof(TEntity)}");
    }
}
