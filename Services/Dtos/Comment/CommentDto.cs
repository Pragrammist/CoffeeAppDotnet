using Domain;
using Services.Dtos.BaseDtos;


namespace Services.Dtos.Comment
{
    public class CommentDto : BaseOutputDto
    {
        public string Text { get; set; } = null!;

        public int Score { get; set; }

        public string Author { get; set; } = null!;

    }
}
