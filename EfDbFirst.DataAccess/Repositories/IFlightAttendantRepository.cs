using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.DataAccess.Repositories;

public interface IFlightAttendantRepository : IBaseRepository<FlightAttendant>
{
    Task<bool> IsMentorAsync(int attendantId);
}
