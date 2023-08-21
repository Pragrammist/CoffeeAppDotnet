using Services.Dtos.Comment;

namespace Services.Dtos.Coffee
{
    public class CoffeeDetailsDto : CoffeeDto
    {
        public IEnumerable<CommentDto> Comments { get; set; } = new List<CommentDto>();

        public IEnumerable<string> Photos { get; set; } = null!;
    }

    
}
