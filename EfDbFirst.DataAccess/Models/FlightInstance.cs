namespace EfDbFirst.DataAccess.Models;

public class FlightInstance
{
    public int InstanceId { get; set; }
    public string FlightNo { get; set; } = null!;
    public int PlaneId { get; set; }
    public int PilotAboardId { get; set; }
    public int CoPilotAboardId { get; set; }
    public int FsmAttendantId { get; set; }
    public DateTime DateTimeLeave { get; set; }
    public DateTime DateTimeArrive { get; set; }
    public virtual Pilot CoPilotAboard { get; set; } = null!;
    public virtual Flight FlightNoNavigation { get; set; } = null!;
    public virtual FlightAttendant FsmAttendant { get; set; } = null!;
    public virtual Pilot PilotAboard { get; set; } = null!;
    public virtual PlaneDetail Plane { get; set; } = null!;
    public virtual ICollection<FlightAttendant> Attendants { get; set; } = new List<FlightAttendant>();
}
