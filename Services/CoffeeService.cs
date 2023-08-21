using EFCore;
using EFCore.Heplers.IQueryableHelpers;
using EFCore.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Services.Common;
using Services.Contracts;
using Services.Dtos.Coffee;

namespace Services
{
    public class CoffeeService : DbServiceBase<CoffeeDetailsDto, Coffee>, ICoffeeService 
    {
        
        public CoffeeService(IRepository repository) : base(repository)
        {
           
        }
        public async Task<CoffeeDetailsDto> CreateCoffee(CreateCoffeeDto data) => 
        await base.Create(data);


        public async Task DeleteCoffee(int coffeeId) => await base.Delete(coffeeId);

            

        public async Task<CoffeeDetailsDto> EditCoffee(EditCoffeeDto data)
        {
            var coffeeEnt = data.Adapt<Coffee>();

            _dbRepository.Context.Attach(coffeeEnt);

            return await base.Edit(coffeeEnt);
        }


        public async Task<CoffeeDetailsDto> GetCoffeeDetails(int coffeeId)
        => (await _dbRepository.Context.Coffees.Include(c => c.Comments)
            .GetById(coffeeId))
            .Adapt<CoffeeDetailsDto>();



        public IQueryable<CoffeeDto> GetCoffees()
        => base.GetItems<CoffeeDto>();
    }
}