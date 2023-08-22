

using Domain.Enums;

namespace Services.Dtos.Comment
{
    public class DeleteCommentDto
    {
        public int CommentId { get; set; }

        public UserRole CurrentUserRole { get; set; }

        public int CurrentUserId { get; set; }
    }
}
