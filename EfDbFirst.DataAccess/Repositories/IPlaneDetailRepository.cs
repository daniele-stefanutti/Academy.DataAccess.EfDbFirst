using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.DataAccess.Repositories;

public interface IPlaneDetailRepository : IBaseRepository<PlaneDetail>
{
    Task<PlaneDetail?> GetByRegistrationNoAsync(string RegistrationNo);
}
