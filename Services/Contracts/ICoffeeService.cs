using Services.Dtos.Coffee;

namespace Services.Contracts
{
    public interface ICoffeeService
    {
        public IQueryable<CoffeeDto> GetCoffees();

        public Task<CoffeeDetailsDto> GetCoffeeDetails(int coffeeId);

        public Task<CoffeeDetailsDto> CreateCoffee(CreateCoffeeDto data);

        public Task DeleteCoffee(int coffeeId);

        public Task<CoffeeDetailsDto> EditCoffee(EditCoffeeDto data);
    }
}