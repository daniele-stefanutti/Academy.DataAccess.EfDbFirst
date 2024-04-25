using EfDbFirst.Business.Dtos;

namespace EfDbFirst.Business.Services;

public interface IFlightService
{
    Task<FlightDto?> GetFlightWithLongestDistanceAsync();
    Task<IReadOnlyList<FlightDto>> GetAllFlightsDepartingFromCountryAsync(string countryCode);
}
