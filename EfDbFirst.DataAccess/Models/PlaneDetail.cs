namespace EfDbFirst.DataAccess.Models;

public class PlaneDetail
{
    public int PlaneId { get; set; }
    public string ModelNumber { get; set; } = null!;
    public string RegistrationNo { get; set; } = null!;
    public short BuiltYear { get; set; }
    public short FirstClassCapacity { get; set; }
    public short EcoCapacity { get; set; }
    public virtual ICollection<FlightInstance> FlightInstances { get; set; } = new List<FlightInstance>();
    public virtual PlaneModel ModelNumberNavigation { get; set; } = null!;
}
