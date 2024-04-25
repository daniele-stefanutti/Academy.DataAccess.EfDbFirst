using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.DataAccess.Repositories;

internal class AirportRepository : BaseRepository, IAirportRepository
{
    /// <remarks>
    /// In modern applications, the needed instances are provided by Dependency Injection (DI) system
    /// </remarks>
    public AirportRepository(AirlineContext context) : base(context)
    { }

    #region READ

    public Airport? GetByAirportCode(string airportCode)
        => Context.Airports.SingleOrDefault(a => a.AirportCode == airportCode);

    public IReadOnlyList<Airport> GetByCountryCode(string countryCode)
        => Context.Airports.Where(a => a.CountryCode == countryCode).ToList();

    /// <remarks>
    /// Please, implement this method!
    /// 
    /// North West
    ///     \_________
    ///     |         |
    ///     |         |
    ///     |         |
    ///     |_________|
    ///                \
    ///            South East
    /// 
    /// </remarks>
    public IReadOnlyList<Airport> GetBySquareArea(double northWestLongitude, double northWestLatitude, double southEastLongitude, double southEastLatitude)
        => Context.Airports.Where(a => a.Longitude >= northWestLongitude && a.Longitude <= southEastLongitude &&
                                        a.Latitude >= northWestLatitude && a.Latitude <= southEastLatitude)
                            .ToList();

    #endregion

    #region CREATE

    /// <returns>Number of affected rows</returns>
    public int Add(Airport airport)
    {
        Context.Airports.Add(airport);
        return Context.SaveChanges();
    }

    /// <returns>Number of affected rows</returns>
    public int AddRange(IReadOnlyList<Airport> airports)
    {
        Context.Airports.AddRange(airports);
        return Context.SaveChanges();
    }

    #endregion

    #region UPDATE

    /// <returns>Number of affected rows</returns>
    public int Update(Airport airport)
    {
        Context.Airports.Update(airport);
        return Context.SaveChanges();
    }

    #endregion

    #region DELETE

    /// <returns>Number of affected rows</returns>
    public int DeleteByAirportCode(string airportCode)
    {
        Airport airport = Context.Airports.Single(a => a.AirportCode == airportCode);
        Context.Airports.Remove(airport);
        return Context.SaveChanges();
    }

    /// <returns>Number of affected rows</returns>
    public int DeleteByCountryCode(string countryCode)
    {
        IEnumerable<Airport> airports = Context.Airports.Where(a => a.CountryCode == countryCode);
        Context.Airports.RemoveRange(airports);
        return Context.SaveChanges();
    }

    #endregion
}
