using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.DataAccess.Repositories;

public interface IFlightInstanceRepository : IBaseRepository<FlightInstance>
{
    Task<FlightInstance?> GetByFlightNoAndDateTimeLeaveAsync(string flightNo, DateTime dateTimeLeave);
    Task<IReadOnlyList<FlightInstance>> GetWithinDateTimeLeaveRangeAsync(DateTime startDateTimeLeave, DateTime endDateTimeLeave);
    Task<IReadOnlyList<FlightInstance>> GetByPlaneManufacturerNameAsync(string planeManufacturerName);
    Task<IReadOnlyList<FlightInstance>> GetByFlightArriveFromAirportCodeAsync(string airportCode);
}
