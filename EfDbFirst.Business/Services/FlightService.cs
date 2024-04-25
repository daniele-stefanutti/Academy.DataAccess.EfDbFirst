using EfDbFirst.Business.Dtos;
using EfDbFirst.Business.Exceptions;
using EfDbFirst.Business.Mappers;
using EfDbFirst.DataAccess.Models;
using EfDbFirst.DataAccess.Repositories;

namespace EfDbFirst.Business.Services;

/// <remarks>
/// This example illustrates a simple asynchronous service with basic errors handling
/// </remarks>
internal sealed class FlightService : BaseService, IFlightService
{
    private readonly IFlightRepository _flightRepository;

    /// <remarks>
    /// In modern services, the needed instances are provided by Dependency Injection (DI) system
    /// </remarks>
    public FlightService(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<FlightDto?> GetFlightWithLongestDistanceAsync()
    {
        try
        {
            Flight? flight = await _flightRepository.GetWithLongestDistanceAsync();
            return flight != null ? FlightMapper.Map(flight) : throw new NoDataException(nameof(Flight));
        }
        catch (Exception ex)
        {
            LogError($"{nameof(GetFlightWithLongestDistanceAsync)} failure: {ex.Message}");
            return null;
        }
    }

    public async Task<IReadOnlyList<FlightDto>> GetAllFlightsDepartingFromCountryAsync(string countryCode)
    {
        try
        {
            IReadOnlyList<Flight> flights = await _flightRepository.GetDepartingFromCountryAsync(countryCode);

            if (!flights.Any())
                throw new NoDataException(nameof(Flight));

            return flights.Select(FlightMapper.Map).ToList();
        }
        catch (Exception ex)
        {
            LogError($"{nameof(GetAllFlightsDepartingFromCountryAsync)} failure: {ex.Message}");
            return Array.Empty<FlightDto>();
        }
    }
}
