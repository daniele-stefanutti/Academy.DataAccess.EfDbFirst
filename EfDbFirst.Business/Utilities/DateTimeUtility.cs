namespace EfDbFirst.Business.Utilities;

internal static class DateTimeUtility
{
    private const int DaysPerYear = 365;

    public static int GetAge(DateTime dateTime)
        => (int)Math.Floor((DateTime.Now - dateTime).TotalDays / DaysPerYear);
}
