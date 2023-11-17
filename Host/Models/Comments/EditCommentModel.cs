namespace Host.Models.Comments
{
    public class EditCommentModel
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int Score { get; set; }

        public IEnumerable<IFormFile>? Photos { get; set; } = null!;
    }
}
