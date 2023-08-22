using static Domain.Consts.SepartaratorsConsts;
namespace Host.Infrastructure.Helpers.PhotoDataHelpers
{
    public static class PhotoDataHelpers
    {
        public static string JoinFileNames(this IEnumerable<IFormFile> files) => String.Join(FILES_SEPORATOR_IN_STORE, files.Select(s => s.FileName));
    }
}
