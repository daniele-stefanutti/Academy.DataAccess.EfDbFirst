using EfDbFirst.Business.Dtos;
using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.Business.Mappers;

internal static class AirportMapper
{
    /// <remarks>
    /// Example
    /// </remarks>
    public static AirportDto Map(Airport airport)
        => new
        (
            Code: airport.AirportCode,
            Longitude: airport.Longitude,
            Latitude: airport.Latitude,
            CountryName: airport.CountryCodeNavigation.CountryName
        );
}
