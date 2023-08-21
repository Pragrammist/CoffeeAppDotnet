namespace EFCore.Models;

public class Coffee : EntityBase
{
    public string Name { get; set; } = null!;

    public string Note { get; set; } = null!;

    public string Photos { get; set; } = null!;

    public float AvgScore { get; set; }

    public int ScoreCount { get; set; }

    public List<Comment> Comments { get; set; } = null!;
}