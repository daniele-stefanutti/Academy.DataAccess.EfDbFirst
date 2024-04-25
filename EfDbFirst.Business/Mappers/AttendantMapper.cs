using EfDbFirst.Business.Dtos;
using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.Business.Mappers;

internal static class AttendantMapper
{
    public static AttendantDto Map(FlightAttendant attendant, bool isMentor)
        => new
        (
            FirstName: attendant.FirstName,
            LastName: attendant.LastName,
            IsMentor: isMentor
        );
}
