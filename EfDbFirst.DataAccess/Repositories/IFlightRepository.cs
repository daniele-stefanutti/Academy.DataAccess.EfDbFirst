using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.DataAccess.Repositories;

public interface IFlightRepository
{
    Task<Flight?> GetWithLongestDistanceAsync();
    Task<IReadOnlyList<Flight>> GetDepartingFromCountryAsync(string countryCode);
}
