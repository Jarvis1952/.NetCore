namespace Entities
{
    /// <summary>
    /// Domain Model for storing the Country
    /// </summary>
    public class Country
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }
    }
}