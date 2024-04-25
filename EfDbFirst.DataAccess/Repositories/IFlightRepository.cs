using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.DataAccess.Repositories;

public interface IFlightRepository : IBaseRepository<Flight>
{
    Task<Flight?> GetWithLongestDistanceAsync();
    Task<IReadOnlyList<Flight>> GetDepartingFromCountryAsync(string countryCode);
}
