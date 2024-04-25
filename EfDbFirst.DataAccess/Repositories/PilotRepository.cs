using EfDbFirst.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDbFirst.DataAccess.Repositories;

internal class PilotRepository : BaseRepository<Pilot>, IPilotRepository
{
    public PilotRepository(AirlineContext context) : base(context)
    { }

    public async Task<Pilot?> GetByFirstNameAndLastNameAsync(string firstName, string lastName)
        => await Context.Pilots.SingleOrDefaultAsync(p => p.FirstName == firstName && p.LastName == lastName);
}
