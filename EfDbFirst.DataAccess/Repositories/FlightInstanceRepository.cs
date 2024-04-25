namespace EfDbFirst.DataAccess.Repositories;

internal class FlightInstanceRepository : BaseRepository, IFlightInstanceRepository
{
    public FlightInstanceRepository(AirlineContext context) : base(context)
    { }

    /// <remarks>
    /// Please, implement this class
    /// </remarks>
}
