using EFCore;
using EFCore.Models;
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
        public async Task<CoffeeDetailsDto> CreateCoffeeDto(CreateCoffeeDto data) => 
        await base.Create(
            data
        );


        public async Task DeleteCoffeeDto(int coffeeId) => await base.Delete(coffeeId);

            

        public async Task<CoffeeDetailsDto> EditCoffee(EditCoffeeDto data) => await base.Edit(data);


        public async Task<CoffeeDetailsDto> GetCoffeeDetails(int coffeeId)
        => await base.GetByIdAsync(coffeeId);
        

        public IQueryable<CoffeeDto> GetCoffees()
        => base.GetItems<CoffeeDto>();
    }
}