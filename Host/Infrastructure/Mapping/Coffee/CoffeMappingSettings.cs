using Host.Models.Coffees;
using Mapster;
using Services.Dtos.Coffee;
using static Domain.Consts.SepartaratorsConsts;

namespace Host.Infrastructure.Mapping.Coffee
{
    public static class CoffeeMappingSettings
    {
        
        public static void SetCoffeeMapping()
        {
            TypeAdapterConfig<EditCoffeeModel, EditCoffeeDto>.NewConfig().Map(d => d.Photos, s => s.Photos.JoinFileNames()); 

            TypeAdapterConfig<CreateCoffeeModel, CreateCoffeeDto>.NewConfig().Map(d => d.Photos, s => s.Photos.JoinFileNames());
        }

        static string JoinFileNames(this IEnumerable<IFormFile> files) => String.Join(FILES_SEPORATOR_IN_STORE, files.Select(s => s.FileName));
    }
}
