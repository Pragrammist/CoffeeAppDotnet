using System.ComponentModel.DataAnnotations;

namespace Host.Models.Comments
{
    public class CreateCommentModel
    {
        public string Text { get; set; } = null!;

        public int Score { get; set; }

        public int CoffeeId { get; set; }

        public IEnumerable<IFormFile>? Photos { get; set; }
    }
}
