namespace EfDbFirst.Business.Dtos;

public sealed record PlaneStatisticsDto
(
    string RegistrationNo,
    string Model,
    short BuiltYear,
    int OverallPlanePilots,
    int OverallFlightInstances,
    int OverallDistance,
    DateTime FirstFlightDateTimeLeave,
    DateTime LastFlightDateTimeLeave
);
