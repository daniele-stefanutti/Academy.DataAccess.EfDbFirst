using EfDbFirst.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDbFirst.DataAccess.Repositories;

internal class PlaneDetailRepository : BaseRepository<PlaneDetail>, IPlaneDetailRepository
{
    public PlaneDetailRepository(AirlineContext context) : base(context)
    { }

    public async Task<PlaneDetail?> GetByRegistrationNoAsync(string RegistrationNo)
        => await Context.PlaneDetails
            .Include(pd => pd.ModelNumberNavigation)
                .ThenInclude(pm => pm.Pilots)
            .SingleOrDefaultAsync(pd => pd.RegistrationNo == RegistrationNo);
}
