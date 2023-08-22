using Domain.Enums;

namespace EFCore.Models;


public class Comment : EntityBase
{
    public string? Photos { get; set; }

    public string Text { get; set; } = null!;

    public int Score { get; set; }

    public CommentType CommentType {get; set; }        

    public int CoffeeId { get; set; }

    public Coffee Coffee { get; set; } = null!;

    public int UserId { get; set; }

    public User User {get; set; } = null!;

    public int? CommentId { get; set; }

    public Comment? CommentToAnswer { get; set; }

    public List<Comment> Comments { get; set;} = new List<Comment>();


}