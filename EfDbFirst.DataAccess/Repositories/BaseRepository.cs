namespace EfDbFirst.DataAccess.Repositories;

internal class BaseRepository
{
    protected readonly AirlineContext Context;

    public BaseRepository(AirlineContext context)
    {
        Context = context;
    }
}
