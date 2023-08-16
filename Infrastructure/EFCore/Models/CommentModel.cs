using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Domain;

namespace EFCore.Models;


public class Comment
{
    public int Id { get; set; }

    [MinLength(3)]
    public string? Photos { get; set; }

    [MaxLength(200), MinLength(4)]
    public string Text { get; set; } = null!;

    [Range(1,5)]
    public int Score { get; set; }

    public DateTime CreationDate { get; set; }

    public CommentType CommentType {get; set; }        

    public DateTime? LastUpdatedDate { get; set; } // изменения пользователем

    public DateTime? LastChangedDate { get; set; } // изменения модератором

    public int CoffeeId { get; set; }

    public Coffee Coffee { get; set; } = null!;

    public int UserId { get; set; }

    public User User {get; set; } = null!;

    public int? CommentId { get; set; }

    

    [NotMapped]
    public bool IsChangedByModerator => LastChangedDate is not null;


}