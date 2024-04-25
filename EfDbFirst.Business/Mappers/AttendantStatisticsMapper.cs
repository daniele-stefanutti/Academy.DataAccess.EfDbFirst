using EfDbFirst.Business.Dtos;
using EfDbFirst.Business.Utilities;
using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.Business.Mappers;

internal static class AttendantStatisticsMapper
{
    public static AttendantStatisticsDto Map(FlightAttendant flightAttendant)
        => new
        (
            FirstName: flightAttendant.FirstName,
            LastName: flightAttendant.LastName,
            Age: DateTimeUtility.GetAge(flightAttendant.Dob),
            HireYear: flightAttendant.HireDate.Year,
            OverallFlights: GetOverallFlights(flightAttendant),
            MentoredAttendants: flightAttendant.InverseMentor.Count
        );

    private static int GetOverallFlights(FlightAttendant flightAttendant)
        => flightAttendant.FlightInstances.Union(flightAttendant.Instances).Distinct().Count();
}
