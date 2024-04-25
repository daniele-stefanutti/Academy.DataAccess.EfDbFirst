using EfDbFirst.Business.Dtos;
using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.Business.Mappers;

internal static class PlaneStatisticsMapper
{
    public static PlaneStatisticsDto Map(PlaneDetail planeDetail, IReadOnlyList<FlightInstance> flightInstances)
        => new
        (
            RegistrationNo: planeDetail.RegistrationNo,
            Model: $"{planeDetail.ModelNumberNavigation.ManufacturerName} {planeDetail.ModelNumber}",
            BuiltYear: planeDetail.BuiltYear,
            OverallPlanePilots: planeDetail.ModelNumberNavigation.Pilots.Count,
            OverallFlightInstances: flightInstances.Count,
            OverallDistance: flightInstances.Sum(fi => fi.FlightNoNavigation.Distance),
            FirstFlightDateTimeLeave: flightInstances.Min(fi => fi.DateTimeLeave),
            LastFlightDateTimeLeave: flightInstances.Max(fi => fi.DateTimeLeave)
        );
}
