using Domain.Options;
using Microsoft.Extensions.Options;
using Services.Contracts;
using Services.Dtos.Files;
using static System.IO.Path;

namespace Services
{
    public class FileService : IFileService
    {
        readonly FileStoringOptions _fileStoringOptions;
        readonly string _rootDirectory;
        public FileService(IOptions<FileStoringOptions> fileStoringOpt)
        {
            _fileStoringOptions = fileStoringOpt.Value;
            _rootDirectory = _fileStoringOptions.RootDircectory;
        }
        public async Task WriteFile(WriteFileDto file)
        {
            using var fileWriter = File.OpenWrite(Combine(_rootDirectory, file.Name));

            await file.FileData.CopyToAsync(fileWriter);

        }

        public async Task WriteFiles(IEnumerable<WriteFileDto> files)
        {
            foreach(var file in files)
            {
                await WriteFile(file);
            }
        }
    }
}
