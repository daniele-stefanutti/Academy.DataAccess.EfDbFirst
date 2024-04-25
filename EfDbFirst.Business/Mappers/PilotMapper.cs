using EfDbFirst.Business.Dtos;
using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.Business.Mappers;

internal static class PilotMapper
{
    public static PilotDto Map(Pilot pilot, int age)
        => new
        (
            FirstName: pilot.FirstName,
            LastName: pilot.LastName,
            Age: age
        );
}
