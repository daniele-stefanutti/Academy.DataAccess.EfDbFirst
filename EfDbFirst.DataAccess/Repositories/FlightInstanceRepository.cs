using EfDbFirst.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDbFirst.DataAccess.Repositories;

internal class FlightInstanceRepository : BaseRepository<FlightInstance>, IFlightInstanceRepository
{
    public FlightInstanceRepository(AirlineContext context) : base(context)
    { }

    public async Task<FlightInstance?> GetByFlightNoAndDateTimeLeaveAsync(string flightNo, DateTime dateTimeLeave)
        => await GetFlightInstancesIncludingDetails()
            .Where(fi => fi.FlightNo == flightNo && fi.DateTimeLeave == dateTimeLeave)
            .SingleOrDefaultAsync();

    public async Task<IReadOnlyList<FlightInstance>> GetWithinDateTimeLeaveRangeAsync(DateTime startDateTimeLeave, DateTime endDateTimeLeave)
        => await GetFlightInstancesIncludingDetails()
            .Where(fi => fi.DateTimeLeave > startDateTimeLeave && fi.DateTimeLeave < endDateTimeLeave)
            .ToListAsync();

    public async Task<IReadOnlyList<FlightInstance>> GetByPlaneManufacturerNameAsync(string planeManufacturerName)
        => await GetFlightInstancesIncludingDetails()
            .Where(fi => fi.Plane.ModelNumberNavigation.ManufacturerName == planeManufacturerName)
            .ToListAsync();

    public async Task<IReadOnlyList<FlightInstance>> GetByFlightArriveFromAirportCodeAsync(string airportCode)
        => await GetFlightInstancesIncludingDetails()
            .Where(fi => fi.FlightNoNavigation.FlightArriveFrom == airportCode)
            .ToListAsync();

    private IQueryable<FlightInstance> GetFlightInstancesIncludingDetails()
        => Context.FlightInstances
            .Include(fi => fi.FlightNoNavigation)
            .Include(fi => fi.Plane)
                .ThenInclude(p => p.ModelNumberNavigation)
            .Include(fi => fi.PilotAboard)
            .Include(fi => fi.FsmAttendant)
            .Include(fi => fi.Attendants);
}
