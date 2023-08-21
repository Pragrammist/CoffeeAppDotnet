namespace Services.Dtos.Coffee
{
    public class EditCoffeeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Note { get; set; } = null!;

        public string Photos { get; set; } = null!;
    }
}
