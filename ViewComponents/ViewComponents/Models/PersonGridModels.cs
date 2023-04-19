namespace ViewComponents.Models
{
    public class PersonGridModels
    {
        public string? GridName { get; set; } = string.Empty;
        public List<Person> Persons { get; set; } = new List<Person>();
    }
}
