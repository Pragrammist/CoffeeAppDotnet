namespace Services.Dtos.Coffee
{
    public class CreateCoffeeDto
    {
        public string Name { get; set; } = null!;

        public string Note { get; set; } = null!;

        public IEnumerable<string> Photos { get; set; } = null!;
    }
}
