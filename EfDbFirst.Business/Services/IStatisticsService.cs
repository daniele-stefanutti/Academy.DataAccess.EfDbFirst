using EfDbFirst.Business.Dtos;

namespace EfDbFirst.Business.Services;

public interface IStatisticsService
{
    Task<PlaneStatisticsDto?> GetPlaneStatisticsAsync(string RegistrationNo);
    Task<IReadOnlyList<AttendantStatisticsDto>> GetAttendantsStatisticsAsync();
}
