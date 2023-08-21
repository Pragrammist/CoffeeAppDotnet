
namespace Services.Dtos.Files
{
    public class WriteFileDto
    {
        public string Name { get; set; } = null!;

        public Stream FileData { get; set; } = null!;
        
    }
}
