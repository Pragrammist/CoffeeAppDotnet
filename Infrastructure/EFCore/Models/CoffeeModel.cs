

using System.ComponentModel.DataAnnotations;

namespace EFCore.Models;
public class Coffee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Note { get; set; } = null!;

    public string Photos { get; set; } = null!;

    public float AvgScore { get; set; }

    public int ScoreCount { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? LastUpdatedDate { get; set; }
}