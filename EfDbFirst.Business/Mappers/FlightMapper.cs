using EfDbFirst.Business.Dtos;
using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.Business.Mappers;

internal static class FlightMapper
{
    /// <remarks>
    /// Example
    /// </remarks>
    public static FlightDto Map(Flight flight)
        => new
        (
            FlightNo: flight.FlightNo,
            DepartureAirport: AirportMapper.Map(flight.FlightArriveFromNavigation),
            ArrivalAirport: AirportMapper.Map(flight.FlightDepartToNavigation),
            Distance: flight.Distance
        );
}
