namespace EfDbFirst.DataAccess.Repositories;

internal class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    protected readonly AirlineContext Context;

    public BaseRepository(AirlineContext context)
    {
        Context = context;
    }

    public async Task<int> UpdateAsync(TEntity entity)
    {
        Context.Update(entity);
        return await Context.SaveChangesAsync();
    }

    public async Task<int> UpdateRangeAsync(IReadOnlyList<TEntity> entities)
    {
        Context.UpdateRange(entities);
        return await Context.SaveChangesAsync();
    }
}
