namespace Employees.Shared.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }
        public Countries Country { get; set; } = null!;
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
