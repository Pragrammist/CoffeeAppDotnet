namespace Host.Models.Comments
{
    public class CreateAnswerToCommentModel
    {
        public int Id { get; set; }


        public string Text { get; set; } = null!;


        public int CommentId { get; set; }
        

        public string Photos { get; set; } = null!;
    }
    
}
