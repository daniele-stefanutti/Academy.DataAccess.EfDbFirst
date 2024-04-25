namespace EfDbFirst.DataAccess.Models;

public class FlightAttendant
{
    public int AttendantId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Dob { get; set; }
    public DateTime HireDate { get; set; }
    public int? MentorId { get; set; }
    public virtual ICollection<FlightInstance> FlightInstances { get; set; } = new List<FlightInstance>();
    public virtual ICollection<FlightAttendant> InverseMentor { get; set; } = new List<FlightAttendant>();
    public virtual FlightAttendant? Mentor { get; set; }
    public virtual ICollection<FlightInstance> Instances { get; set; } = new List<FlightInstance>();
}
