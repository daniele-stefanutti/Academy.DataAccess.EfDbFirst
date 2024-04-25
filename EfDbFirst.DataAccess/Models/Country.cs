namespace EfDbFirst.DataAccess.Models;

public class Country
{
    public string CountryName { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
    public virtual ICollection<Airport> Airports { get; set; } = new List<Airport>();
}
