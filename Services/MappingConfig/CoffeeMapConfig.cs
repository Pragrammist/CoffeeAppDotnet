using EFCore.Models;
using Mapster;
using Services.Dtos.Coffee;
using static Domain.Consts.SepartaratorsConsts;

namespace Services.MappingConfig
{
    public static class CoffeeMapConfig
    {

        public static void SetCoffeeMapping()
        {
            TypeAdapterConfig<Coffee, CoffeeDto>.NewConfig().Map(d => d.Photo, s => s.SplitPhotos().First());

            TypeAdapterConfig<Coffee, CoffeeDetailsDto>.NewConfig().Map(d => d.Photos, s => s.SplitPhotos());
            
            //.AfterMapping((source,dest) =>
            //{
            //dest.Photos = source.SplitPhotos();
            //});


        }

        static IEnumerable<string> SplitPhotos(this Coffee coffee) => coffee.Photos.Split(FILES_SEPORATOR_IN_STORE);

        
    }

    
}
