using EfDbFirst.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDbFirst.DataAccess.Repositories;

internal class FlightRepository : BaseRepository, IFlightRepository
{
    public FlightRepository(AirlineContext context) : base(context)
    { }

    #region READ

    public async Task<Flight?> GetWithLongestDistanceAsync()
        => await GetFlightsIncludingAirportsAndCountries()
            .OrderByDescending(f => f.Distance)
            .FirstOrDefaultAsync();

    public async Task<IReadOnlyList<Flight>> GetDepartingFromCountryAsync(string countryCode)
        => await GetFlightsIncludingAirportsAndCountries()
            .Where(f => f.FlightArriveFromNavigation.CountryCode == countryCode)
            .ToListAsync();

    private IQueryable<Flight> GetFlightsIncludingAirportsAndCountries()
        => Context.Flights
            .Include(f => f.FlightArriveFromNavigation)
                .ThenInclude(a => a.CountryCodeNavigation)
            .Include(f => f.FlightDepartToNavigation)
                .ThenInclude(a => a.CountryCodeNavigation);

    #endregion
}
