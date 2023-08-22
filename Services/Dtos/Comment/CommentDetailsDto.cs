


namespace Services.Dtos.Comment
{
    public class CommentDetailsDto : CommentDto
    {
        public IEnumerable<string> Photos { get; set; } = null!;

        public List<CommentDto> Comments { get; set; } = null!;


    }
}
