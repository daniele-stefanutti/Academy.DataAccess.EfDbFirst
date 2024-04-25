using EfDbFirst.Business.Dtos;
using EfDbFirst.Business.Exceptions;
using EfDbFirst.Business.Mappers;
using EfDbFirst.DataAccess.Models;
using EfDbFirst.DataAccess.Repositories;

namespace EfDbFirst.Business.Services;

internal sealed class StatisticsService : BaseService, IStatisticsService
{
    private readonly IPlaneDetailRepository _planeDetailRepository;
    private readonly IFlightInstanceRepository _flightInstanceRepository;
    private readonly IFlightAttendantRepository _flightAttendantRepository;

    public StatisticsService(IPlaneDetailRepository planeDetailRepository, IFlightInstanceRepository flightInstanceRepository,
        IFlightAttendantRepository flightAttendantRepository)
    {
        _planeDetailRepository = planeDetailRepository;
        _flightInstanceRepository = flightInstanceRepository;
        _flightAttendantRepository = flightAttendantRepository;
    }

    public async Task<PlaneStatisticsDto?> GetPlaneStatisticsAsync(string RegistrationNo)
    {
        try
        {
            PlaneDetail planeDetail = await _planeDetailRepository.GetByRegistrationNoAsync(RegistrationNo)
                ?? throw new ItemNotFoundException(nameof(PlaneDetail), RegistrationNo);

            IReadOnlyList<FlightInstance> flightInstances = await _flightInstanceRepository.GetByPlaneRegistrationNoAsync(RegistrationNo);

            return PlaneStatisticsMapper.Map(planeDetail, flightInstances);
        }
        catch (Exception ex)
        {
            LogError($"{nameof(GetPlaneStatisticsAsync)} failure: {ex.Message}");
            return null;
        }
    }

    public async Task<IReadOnlyList<AttendantStatisticsDto>> GetAttendantsStatisticsAsync()
    {
        try
        {
            IReadOnlyList<FlightAttendant> flightAttendants = await _flightAttendantRepository.GetAllWithFlightInstancesAsync();

            if (!flightAttendants.Any())
                throw new NoDataException(nameof(FlightAttendant));

            return flightAttendants.Select(AttendantStatisticsMapper.Map).ToList();
        }
        catch (Exception ex)
        {
            LogError($"{nameof(GetAttendantsStatisticsAsync)} failure: {ex.Message}");
            return Array.Empty<AttendantStatisticsDto>();
        }
    }
}
