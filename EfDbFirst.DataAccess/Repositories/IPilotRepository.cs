using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.DataAccess.Repositories;

public interface IPilotRepository : IBaseRepository<Pilot>
{
    Task<Pilot?> GetByFirstNameAndLastNameAsync(string firstName, string lastName);
}
