namespace EfDbFirst.DataAccess.Models;

public class PlaneModel
{
    public string ModelNumber { get; set; } = null!;
    public string ManufacturerName { get; set; } = null!;
    public short PlaneRange { get; set; }
    public short CruiseSpeed { get; set; }
    public virtual ICollection<PlaneDetail> PlaneDetails { get; set; } = new List<PlaneDetail>();
    public virtual ICollection<Pilot> Pilots { get; set; } = new List<Pilot>();
}
