namespace EfDbFirst.DataAccess.Repositories;

internal class FlightAttendantRepository : BaseRepository, IFlightAttendantRepository
{
    public FlightAttendantRepository(AirlineContext context) : base(context)
    { }

    /// <remarks>
    /// Please, implement this class
    /// </remarks>
}
