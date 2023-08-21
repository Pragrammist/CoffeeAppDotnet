using Services.Dtos.BaseDtos;

namespace Services.Dtos.Coffee
{
    public class CoffeeDto : BaseOutputDto
    {
        public string Name { get; set; } = null!;

        public string Note { get; set; } = null!;

        public string Photo { get; set; } = null!;

        public float AvgScore { get; set; }

        public int ScoreCount { get; set; }
    }
}
