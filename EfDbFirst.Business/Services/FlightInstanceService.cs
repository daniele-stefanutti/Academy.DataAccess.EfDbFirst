using EfDbFirst.Business.Dtos;
using EfDbFirst.Business.Exceptions;
using EfDbFirst.Business.Mappers;
using EfDbFirst.DataAccess.Models;
using EfDbFirst.DataAccess.Repositories;

namespace EfDbFirst.Business.Services;

internal sealed class FlightInstanceService : BaseService, IFlightInstanceService
{
    private readonly IFlightInstanceRepository _flightInstanceRepository;
    private readonly IPilotRepository _pilotRepository;
    private readonly IFlightAttendantRepository _flightAttendantRepository;
    private readonly IFlightRepository _flightRepository;

    public FlightInstanceService(IFlightInstanceRepository flightInstanceRepository, IPilotRepository pilotRepository,
        IFlightAttendantRepository flightAttendantRepository, IFlightRepository flightRepository)
    {
        _flightInstanceRepository = flightInstanceRepository;
        _pilotRepository = pilotRepository;
        _flightAttendantRepository = flightAttendantRepository;
        _flightRepository = flightRepository;
    }

    /// <summary>
    /// Retrieve flight instances having leaving date and time within the given range
    /// </summary>
    /// <param name="startDateTimeLeave">Beginning date and time of the range</param>
    /// <param name="endDateTimeLeave">Ending date and time of the range</param>
    public async Task<IReadOnlyList<FlightInstanceDto>> GetAllFlightInstancesWithinDateTimeLeaveRangeAsync(DateTime startDateTimeLeave, DateTime endDateTimeLeave)
    {
        try
        {
            IReadOnlyList<FlightInstance> flightInstances = await _flightInstanceRepository.GetWithinDateTimeLeaveRangeAsync(startDateTimeLeave, endDateTimeLeave);

            if (!flightInstances.Any())
                throw new NoDataException(nameof(FlightInstance));

            return await GetFlightInstanceDtosAsync(flightInstances);
        }
        catch (Exception ex)
        {
            LogError($"{nameof(GetAllFlightInstancesWithinDateTimeLeaveRangeAsync)} failure: {ex.Message}");
            return Array.Empty<FlightInstanceDto>();
        }
    }

    /// <summary>
    /// Retrieve flight instances served by a plane produced by the given manufacturer
    /// </summary>
    /// <param name="planeManufacturerName">Name of the plane's manufacturer</param>
    public async Task<IReadOnlyList<FlightInstanceDto>> GetAllFlightInstancesServedByPlaneManufacturerAsync(string planeManufacturerName)
    {
        try
        {
            IReadOnlyList<FlightInstance> flightInstances = await _flightInstanceRepository.GetByPlaneManufacturerNameAsync(planeManufacturerName);

            if (!flightInstances.Any())
                throw new NoDataException(nameof(FlightInstance));

            return await GetFlightInstanceDtosAsync(flightInstances);
        }
        catch (Exception ex)
        {
            LogError($"{nameof(GetAllFlightInstancesServedByPlaneManufacturerAsync)} failure: {ex.Message}");
            return Array.Empty<FlightInstanceDto>();
        }
    }

    /// <summary>
    /// Update the co-pilot for the flight instance having the given flight number and departure date and time
    /// </summary>
    /// <param name="flightNo">Given flight number</param>
    /// <param name="dateTimeLeave">Given departure date and time</param>
    /// <param name="coPilotFirstName">First name of the updated co-pilot</param>
    /// <param name="coPilotLastName">Last name of the updated co-pilot</param>
    /// <returns>Number of affected rows</returns>
    public async Task<int> UpdateFlightInstanceCoPilotAsync(string flightNo, DateTime dateTimeLeave, string coPilotFirstName, string coPilotLastName)
    {
        try
        {
            FlightInstance flightInstance = await _flightInstanceRepository.GetByFlightNoAndDateTimeLeaveAsync(flightNo, dateTimeLeave)
                ?? throw new NoDataException(nameof(FlightInstance));

            Pilot coPilot = await _pilotRepository.GetByFirstNameAndLastNameAsync(coPilotFirstName, coPilotLastName)
                ?? throw new ItemNotFoundException(nameof(Pilot), $"{coPilotFirstName} {coPilotLastName}");

            flightInstance.CoPilotAboardId = coPilot.PilotId;

            return await _flightInstanceRepository.UpdateAsync(flightInstance);
        }
        catch (Exception ex)
        {
            LogError($"{nameof(UpdateFlightInstanceCoPilotAsync)} failure: {ex.Message}");
            return 0;
        }
    }

    /// <summary>
    /// Update the arrival date and time for the flight instances departing from the given airport by setting the given delay
    /// </summary>
    /// <param name="airportCode">Code of the departure airport</param>
    /// <param name="delay">Delay on arrival date and time</param>
    /// <returns>Number of affected rows</returns>
    public async Task<int> SetDelayForFlightInstancesArrivingFromAirportAsync(string airportCode, TimeSpan delay)
    {
        try
        {
            if (delay.TotalHours == 0)
                throw new InvalidOperationException("Delay cannot be zero");

            IReadOnlyList<FlightInstance> flightInstances = await _flightInstanceRepository.GetByFlightArriveFromAirportCodeAsync(airportCode);

            if (!flightInstances.Any())
                throw new NoDataException(nameof(FlightInstance));

            foreach (FlightInstance flightInstance in flightInstances)
                flightInstance.DateTimeArrive += delay;

            return await _flightInstanceRepository.UpdateRangeAsync(flightInstances);
        }
        catch (Exception ex)
        {
            LogError($"{nameof(SetDelayForFlightInstancesArrivingFromAirportAsync)} failure: {ex.Message}");
            return 0;
        }
    }

    #region Locals

    private async Task<IReadOnlyList<FlightInstanceDto>> GetFlightInstanceDtosAsync(IReadOnlyList<FlightInstance> flightInstances)
    {
        List<FlightInstanceDto> results = new();

        foreach (FlightInstance flightInstance in flightInstances)
            results.Add(await GetFlightInstanceDtoAsync(flightInstance));

        return results;
    }

    private async Task<FlightInstanceDto> GetFlightInstanceDtoAsync(FlightInstance flightInstance)
    {
        PlaneDto planeDto = PlaneMapper.Map(flightInstance.Plane);
        PilotDto pilotDto = GetPilotDto(flightInstance.PilotAboard);
        IReadOnlyList<AttendantDto> allAttendantsDtos = await GetAllAttendantsDtosAsync(flightInstance);

        return FlightInstanceMapper.Map(flightInstance, planeDto, pilotDto, allAttendantsDtos);
    }

    private static PilotDto GetPilotDto(Pilot pilot)
    {
        int age = (int)Math.Floor((DateTime.Now - pilot.Dob).TotalDays / 365);
        return PilotMapper.Map(pilot, age);
    }

    private async Task<IReadOnlyList<AttendantDto>> GetAllAttendantsDtosAsync(FlightInstance flightInstance)
    {
        IReadOnlyList<FlightAttendant> flightAttendants = flightInstance.Attendants
            .Union(new FlightAttendant[] { flightInstance.FsmAttendant })
            .DistinctBy(a => a.AttendantId)
            .ToList();
        List<AttendantDto> attendantDtos = new();

        foreach (FlightAttendant flightAttendant in flightAttendants)
            attendantDtos.Add(await GetAttendantDtoAsync(flightAttendant));

        return attendantDtos;
    }

    private async Task<AttendantDto> GetAttendantDtoAsync(FlightAttendant flightAttendant)
    {
        bool isMentor = await _flightAttendantRepository.IsMentorAsync(flightAttendant.AttendantId);
        return AttendantMapper.Map(flightAttendant, isMentor);
    }

    #endregion
}
