using EfDbFirst.Business.Dtos;
using EfDbFirst.Business.Services;
using EfDbFirst.DataAccess;
using EfDbFirst.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EfDbFirst.Tests;

/// <summary>
/// In order to complete the exercises, implement the required methods in the specified Service and Repository classes.
/// Based on your implementation choices, update the class constructor by adding needed repositories to StatisticsService
/// constructor call.
/// Next, run the tests to check if implemented methods work as expected.
/// </summary>
public sealed class Exercise3
{
    private readonly AirlineContext _context;
    private readonly StatisticsService _statisticsService;

    public Exercise3()
    {
        _context = new(new DbContextOptions<AirlineContext>());
        _statisticsService = new
        (
            // Add needed repositiories instances here
        );
    }

    /// <summary>
    /// ### DO NOT CHANGE THE CONTENT OF THE FOLLOWING TEST METHODS ###
    /// </summary>

    #region 1. StatisticsService - GetPlaneStatisticsAsync

    [Fact]
    public async Task Ex11_Implement_GetPlaneStatisticsAsync_method_of_StatisticsService()
    {
        // Arrange
        const string RegistrationNo = "AU-1880";

        PlaneStatisticsDto expected = new
        (
            RegistrationNo: "AU-1880",
            Model: "Airbus A340",
            BuiltYear: 1880,
            OverallPlanePilots: 6,
            OverallFlightInstances: 5,
            OverallDistance: 30100,
            FirstFlightDateTimeLeave: new DateTime(2015, 12, 10, 10, 30, 0),
            LastFlightDateTimeLeave: new DateTime(2015, 12, 12, 10, 30, 0)
        );

        // Act
        PlaneStatisticsDto? actual = await _statisticsService.GetPlaneStatisticsAsync(RegistrationNo);

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Ex12_Implement_GetPlaneStatisticsAsync_method_of_StatisticsService()
    {
        // Arrange
        const string RegistrationNo = "MS-001";

        // Act
        PlaneStatisticsDto? actual = await _statisticsService.GetPlaneStatisticsAsync(RegistrationNo);

        // Assert
        Assert.Null(actual);
        Assert.Equal("GetPlaneStatisticsAsync failure: PlaneDetail with code 'MS-001' not found", _statisticsService.ErrorMessage);
    }

    #endregion

    #region 2. StatisticsService - GetAttendantsStatisticsAsync

    /// <summary>
    /// HINT: Try using GroupBy method of LINQ to Entities in needed repository implementation
    /// </summary>
    [Fact]
    public async Task Ex21_Implement_GetAttendantsStatisticsAsync_method_of_StatisticsService()
    {
        // Arrange
        IReadOnlyList<AttendantStatisticsDto> expected = new AttendantStatisticsDto[]
        {
            new(
                FirstName: "Pramesh",
                LastName: "Shrestha",
                Age: 35,
                HireYear: 2004,
                OverallFlights: 7,
                MentoredAttendants: 2
            ),
            new(
                FirstName: "John",
                LastName: "Rai",
                Age: 45,
                HireYear: 2005,
                OverallFlights: 8,
                MentoredAttendants: 1
            ),
            new(
                FirstName: "Mike",
                LastName: "Magar",
                Age: 34,
                HireYear: 2010,
                OverallFlights: 4,
                MentoredAttendants: 2
            ),
            new(
                FirstName: "Hari",
                LastName: "Cobin",
                Age: 34,
                HireYear: 2003,
                OverallFlights: 5,
                MentoredAttendants: 1
            ),
            new(
                FirstName: "Greg",
                LastName: "Nepal",
                Age: 33,
                HireYear: 2002,
                OverallFlights: 7,
                MentoredAttendants: 1
            ),
            new(
                FirstName: "Ram",
                LastName: "Sharma",
                Age: 26,
                HireYear: 2001,
                OverallFlights: 2,
                MentoredAttendants: 1
            ),
            new(
                FirstName: "Amol",
                LastName: "Pokharel",
                Age: 44,
                HireYear: 2008,
                OverallFlights: 4,
                MentoredAttendants: 1
            ),
            new(
                FirstName: "Nishesh",
                LastName: "Gajurel",
                Age: 125,
                HireYear: 2009,
                OverallFlights: 2,
                MentoredAttendants: 0
            ),
            new(
                FirstName: "Pratik",
                LastName: "Shrestha",
                Age: 25,
                HireYear: 2003,
                OverallFlights: 3,
                MentoredAttendants: 0
            )
        };

        // Act
        IReadOnlyList<AttendantStatisticsDto> actual = await _statisticsService.GetAttendantsStatisticsAsync();

        // Assert
        AssertAreEqualAttendantStatisticsDtoLists(expected, actual);
    }

    #endregion

    #region Locals

    private static void AssertAreEqualAttendantStatisticsDtoLists(IReadOnlyList<AttendantStatisticsDto> expected, IReadOnlyList<AttendantStatisticsDto> actual)
    {
        Assert.Equal(expected.Count, actual.Count);

        foreach (AttendantStatisticsDto expectedItem in expected)
        {
            AttendantStatisticsDto? actualItem = actual.SingleOrDefault(a => a.FirstName == expectedItem.FirstName && a.LastName == expectedItem.LastName);
            Assert.NotNull(actualItem);
            Assert.Equal(expectedItem, actualItem);
        }
    }

    #endregion
}