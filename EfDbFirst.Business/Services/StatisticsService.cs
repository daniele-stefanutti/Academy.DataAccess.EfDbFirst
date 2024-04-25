using EfDbFirst.Business.Dtos;

namespace EfDbFirst.Business.Services;

/// <remarks>
/// For the implementation of the methods in this class:
///     + implement needed Repository classes
///     + implement needed Mappers classes
///     + implement service class methods by using asynchronous ADO.NET methods
/// </remarks>
internal sealed class StatisticsService : BaseService, IStatisticsService
{
    public Task<PlaneStatisticsDto?> GetPlaneStatisticsAsync(string RegistrationNo) => throw new NotImplementedException();
    public Task<IReadOnlyList<AttendantStatisticsDto>> GetAttendantsStatisticsAsync() => throw new NotImplementedException();
}
