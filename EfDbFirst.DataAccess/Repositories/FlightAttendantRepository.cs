using EfDbFirst.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDbFirst.DataAccess.Repositories;

internal class FlightAttendantRepository : BaseRepository<FlightAttendant>, IFlightAttendantRepository
{
    public FlightAttendantRepository(AirlineContext context) : base(context)
    { }

    public async Task<IReadOnlyList<FlightAttendant>> GetAllWithFlightInstancesAsync()
        => await Context.FlightAttendants
            .Include(fa => fa.FlightInstances)
            .Include(fa => fa.Instances)
            .Include(fa => fa.InverseMentor)
            .ToListAsync();

    public async Task<bool> IsMentorAsync(int attendantId)
        => await Context.FlightAttendants.AnyAsync(fi => fi.MentorId == attendantId);
}
