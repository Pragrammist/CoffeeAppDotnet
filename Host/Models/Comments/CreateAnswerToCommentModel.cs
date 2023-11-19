namespace Host.Models.Comments
{
    public class CreateAnswerToCommentModel
    {
        public string Text { get; set; } = null!;


        public int CommentId { get; set; }
    }
    
}
