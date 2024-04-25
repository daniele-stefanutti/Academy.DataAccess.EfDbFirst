namespace EfDbFirst.DataAccess.Models;

public class Airport
{
    public string AirportCode { get; set; } = null!;
    public string AirportName { get; set; } = null!;
    public decimal ContactNo { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string CountryCode { get; set; } = null!;
    public virtual Country CountryCodeNavigation { get; set; } = null!;
    public virtual ICollection<Flight> FlightFlightArriveFromNavigations { get; set; } = new List<Flight>();
    public virtual ICollection<Flight> FlightFlightDepartToNavigations { get; set; } = new List<Flight>();
}
