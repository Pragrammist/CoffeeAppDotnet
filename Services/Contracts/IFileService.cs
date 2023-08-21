

using Services.Dtos.Files;

namespace Services.Contracts
{
    public interface IFileService
    {
        public Task WriteFile(WriteFileDto file);

        public Task WriteFiles(IEnumerable<WriteFileDto> files);
    }
}
