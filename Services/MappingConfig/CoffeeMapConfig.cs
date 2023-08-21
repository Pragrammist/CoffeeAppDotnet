using EFCore.Models;
using Mapster;
using Services.Dtos.Coffee;
using Services.Dtos.Comment;

namespace Services.MappingConfig
{
    public static class CoffeeMapConfig
    {
        const char SEPARATOR = ';';
        public static void ConfigCoffeeMapping()
        {
            TypeAdapterConfig<CreateCoffeeDto, Coffee>.NewConfig().AfterMapping((source, dest) =>
            {
                dest.Photos = String.Join(SEPARATOR, source.Photos);
            });

            TypeAdapterConfig<Coffee, CoffeeDto>.NewConfig().AfterMapping((source, dest) =>
            {
                dest.Photo = source.SplitPhotos().First();
            });

            TypeAdapterConfig<Coffee, CoffeeDetailsDto>.NewConfig().AfterMapping((source,dest) =>
            {
                dest.Photos = source.SplitPhotos();
            });


            TypeAdapterConfig<EditCoffeeDto, Coffee>.NewConfig().AfterMapping((source, dest) =>
            {
                dest.Photos = String.Join(SEPARATOR, source.Photos);
            });

        }

        static IEnumerable<string> SplitPhotos(this Coffee coffee) => coffee.Photos.Split(SEPARATOR);

        
    }

    
}
