using Services.Dtos.Coffee;

namespace Services.Contracts
{
    public interface ICoffeeService
    {
        public IQueryable<CoffeeDto> GetCoffees();

        public Task<CoffeeDetailsDto> GetCoffeeDetails(int coffeeId);

        public Task<CoffeeDetailsDto> CreateCoffeeDto(CreateCoffeeDto data);

        public Task DeleteCoffeeDto(int coffeeId);

        public Task<CoffeeDetailsDto> EditCoffee(EditCoffeeDto data);
    }
}