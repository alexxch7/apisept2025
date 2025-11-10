namespace Employees.Shared.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int StateId { get; set; }
        public State State { get; set; } = null!;
    }
}
