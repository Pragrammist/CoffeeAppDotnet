using EFCore.Models;
using Mapster;
using Services.Dtos.Coffee;
using Services.Helpers;

namespace Services.MappingConfig
{
    public static class CoffeeMapConfig
    {

        public static void SetCoffeeMapping()
        {
            TypeAdapterConfig<Coffee, CoffeeDto>.NewConfig().Map(d => d.Photo, s => s.SplitPhotos().First());

            TypeAdapterConfig<Coffee, CoffeeDetailsDto>.NewConfig().Map(d => d.Photos, s => s.SplitPhotos());
        }

        

        
    }

    
}
