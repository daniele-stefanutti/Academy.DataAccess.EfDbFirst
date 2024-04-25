namespace EfDbFirst.DataAccess.Models;

public class Flight
{
    public string FlightNo { get; set; } = null!;
    public string FlightDepartTo { get; set; } = null!;
    public string FlightArriveFrom { get; set; } = null!;
    public int Distance { get; set; }
    public virtual Airport FlightArriveFromNavigation { get; set; } = null!;
    public virtual Airport FlightDepartToNavigation { get; set; } = null!;
    public virtual ICollection<FlightInstance> FlightInstances { get; set; } = new List<FlightInstance>();
}
