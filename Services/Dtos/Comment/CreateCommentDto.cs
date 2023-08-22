using Domain.Enums;

namespace Services.Dtos.Comment
{
    public class CreateCommentDto
    {
        public string Text { get; set; } = null!;

        public int Score { get; set; }

        public int CoffeeId { get; set; }

        public int UserId { get; set; }

        public string Photos { get; set; } = null!;
    }
}
