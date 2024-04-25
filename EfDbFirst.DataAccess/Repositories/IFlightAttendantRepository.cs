using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.DataAccess.Repositories;

public interface IFlightAttendantRepository : IBaseRepository<FlightAttendant>
{
    Task<IReadOnlyList<FlightAttendant>> GetAllWithFlightInstancesAsync();
    Task<bool> IsMentorAsync(int attendantId);
}
