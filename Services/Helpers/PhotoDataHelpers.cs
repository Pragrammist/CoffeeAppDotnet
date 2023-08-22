using static Domain.Consts.SepartaratorsConsts;
using EFCore.Models;

namespace Services.Helpers
{
    public static class PhotoDataHelpers
    {
        public static IEnumerable<string> SplitPhotos(this Coffee coffee) => coffee.Photos.SplitPhotos();

        public static IEnumerable<string> SplitPhotos(this Comment coffee) => coffee?.Photos?.SplitPhotos() ?? Enumerable.Empty<string>();

        public static IEnumerable<string> SplitPhotos(this string photos) => photos.Split(FILES_SEPORATOR_IN_STORE);

    }
}
