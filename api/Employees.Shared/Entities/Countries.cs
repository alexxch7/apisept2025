namespace Employees.Shared.Entities
{
    public class Countries
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<State> States { get; set; } = new List<State>();
    }
}
