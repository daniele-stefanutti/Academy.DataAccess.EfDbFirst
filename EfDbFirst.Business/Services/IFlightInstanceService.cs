using EfDbFirst.Business.Dtos;

namespace EfDbFirst.Business.Services;

public interface IFlightInstanceService
{
    Task<IReadOnlyList<FlightInstanceDto>> GetAllFlightInstancesWithinDateTimeLeaveRangeAsync(DateTime startDateTimeLeave, DateTime endDateTimeLeave);
    Task<IReadOnlyList<FlightInstanceDto>> GetAllFlightInstancesServedByPlaneManufacturerAsync(string planeManufacturerName);
    Task<int> UpdateFlightInstanceCoPilotAsync(string flightNo, DateTime dateTimeLeave, string coPilotFirstName, string coPilotLastName);
    Task<int> SetDelayForFlightInstancesArrivingFromAirportAsync(string airportCode, TimeSpan delay);
}
