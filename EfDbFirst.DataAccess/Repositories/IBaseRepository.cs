namespace EfDbFirst.DataAccess.Repositories;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    Task<int> UpdateAsync(TEntity flightInstance);
    Task<int> UpdateRangeAsync(IReadOnlyList<TEntity> flightInstances);
}
