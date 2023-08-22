using Host.Models.Coffees;
using Mapster;
using Services.Dtos.Coffee;
using Host.Infrastructure.Helpers.PhotoDataHelpers;

namespace Host.Infrastructure.Mapping.Coffee
{
    public static class CoffeeMappingSettings
    {
        
        public static void SetCoffeeMapping()
        {
            TypeAdapterConfig<EditCoffeeModel, EditCoffeeDto>.NewConfig().Map(d => d.Photos, s => s.Photos.JoinFileNames()); 

            TypeAdapterConfig<CreateCoffeeModel, CreateCoffeeDto>.NewConfig().Map(d => d.Photos, s => s.Photos.JoinFileNames());
        }

        
    }
}
