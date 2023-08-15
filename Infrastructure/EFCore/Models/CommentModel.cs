using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace EFCore.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string? Photos { get; set; }

        public string Text { get; set; } = null!;

        public int Score { get; set; }

        public DateTime CreationDate { get; set; }

        public CommentType CommentType {get; set; }        

        public DateTime? LastUpdatedDate { get; set; } // изменения пользователем

        public DateTime? LastChangedDate { get; set; } // изменения модератором

        public int CoffeeId { get; set; }

        public int UserId { get; set; }

        public int? CommentId { get; set; }

        [NotMapped]
        public bool IsChangedByModerator => LastChangedDate is not null;


    }

}