namespace EfDbFirst.Business.Dtos;

/// <param name="MentoredAttendants">Number of attendants which reference this attendant as mentor</param>
public sealed record AttendantStatisticsDto
(
    string FirstName,
    string LastName,
    int Age,
    int HireYear,
    int OverallFlights,
    int MentoredAttendants
);
