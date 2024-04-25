using EfDbFirst.Business.Dtos;
using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.Business.Mappers;

internal static class FlightInstanceMapper
{
    public static FlightInstanceDto Map(FlightInstance flightInstance, PlaneDto planeDto, PilotDto pilotDto, IReadOnlyList<AttendantDto> allAttendantsDtos)
        => new
        (
            FlightNo: flightInstance.FlightNo,
            DepartTo: flightInstance.FlightNoNavigation.FlightDepartTo,
            ArriveFrom: flightInstance.FlightNoNavigation.FlightArriveFrom,
            DateTimeLeave: flightInstance.DateTimeLeave,
            DateTimeArrive: flightInstance.DateTimeArrive,
            Plane: planeDto,
            Pilot: pilotDto,
            AllAttendants: allAttendantsDtos
        );
}
