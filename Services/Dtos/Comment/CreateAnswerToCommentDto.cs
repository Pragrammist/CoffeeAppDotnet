using Domain.Enums;

namespace Services.Dtos.Comment
{
    public class CreateAnswerToCommentDto
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int CommentId { get; set; }

        public int UserId { get; set; }

        public UserRole CurrentUserRole { get; set; }

    }
}
