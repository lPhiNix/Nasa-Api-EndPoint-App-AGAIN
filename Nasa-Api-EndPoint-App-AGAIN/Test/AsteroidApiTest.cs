using Moq;
using Xunit;
using Nasa_Api_EndPoint_App_AGAIN.Client;
using Nasa_Api_EndPoint_App_AGAIN.Dto;

namespace Nasa_Api_EndPoint_App_AGAIN.Tests.Services;

/// <summary>
/// Unit tests for the AsteroidService class.
/// These tests validate filtering and sorting of hazardous asteroids
/// based on data received from the NASA API client.
/// </summary>
public class AsteroidServiceTests
{
    // Mock of the NASA API client
    private readonly Mock<INasaApiClient> _mockClient;
    
    // Instance of the service under test
    private readonly AsteroidService _service;

    public AsteroidServiceTests()
    {
        // Initialize the mock and inject it into the service
        _mockClient = new Mock<INasaApiClient>();
        _service = new AsteroidService(_mockClient.Object);
    }

    /// <summary>
    /// Test that the service returns the top 3 largest hazardous asteroids
    /// for a given number of past days (in this case, 1 day).
    /// </summary>
    [Fact]
    public async Task GetTopHazardousAsteroidsAsync_ReturnsTop3HazardousAsteroids()
    {
        // Arrange: Create fake asteroid data with 4 hazardous entries
        var fakeData = new NasaApiResponse
        {
            NearEarthObjects = new Dictionary<string, List<Asteroid>>
            {
                {
                    DateTime.UtcNow.ToString("yyyy-MM-dd"), [
                        CreateHazardousAsteroid("Ast1", 1.1, 2.1, "2025-05-01", "Earth", "1234.5"),
                        CreateHazardousAsteroid("Ast2", 2.1, 3.1, "2025-05-01", "Earth", "2234.5"),
                        CreateHazardousAsteroid("Ast3", 3.1, 4.1, "2025-05-01", "Earth", "3234.5"),
                        CreateHazardousAsteroid("Ast4", 0.1, 0.2, "2025-05-01", "Earth", "5234.5")
                    ]
                }
            }
        };

        // Set up the mocked client to return fake data
        _mockClient.Setup(client => client.GetNeoFeedAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync(fakeData);

        // Act: Call the method under test
        var result = (await _service.GetTopHazardousAsteroidsAsync(1))?.ToList();

        // Assert: Make sure we get the top 3 hazardous asteroids
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Contains(result, a => a.Name == "Ast1");
        Assert.Contains(result, a => a.Name == "Ast2");
        Assert.Contains(result, a => a.Name == "Ast3");
    }

    /// <summary>
    /// Test that the service returns an empty list when there are no hazardous asteroids.
    /// </summary>
    [Fact]
    public async Task GetTopHazardousAsteroidsAsync_ReturnsEmpty_WhenNoHazardousAsteroids()
    {
        // Arrange: Create fake data with a single non-hazardous asteroid
        var fakeData = new NasaApiResponse
        {
            NearEarthObjects = new Dictionary<string, List<Asteroid>>
            {
                {
                    DateTime.UtcNow.ToString("yyyy-MM-dd"), [
                        new Asteroid
                        {
                            Name = "SafeAsteroid",
                            IsPotentiallyHazardousAsteroid = false,
                            EstimatedDiameter = new EstimatedDiameter
                            {
                                Kilometers = new DiameterKilometers
                                {
                                    EstimatedDiameterMin = 0.1,
                                    EstimatedDiameterMax = 0.3
                                }
                            },
                            CloseApproachData =
                            [
                                new CloseApproachData
                                {
                                    CloseApproachDate = "2025-05-01",
                                    OrbitingBody = "Earth",
                                    RelativeVelocity = new RelativeVelocity
                                    {
                                        KilometersPerHour = "1234"
                                    }
                                }
                            ]
                        }
                    ]
                }
            }
        };

        // Set up the mocked client to return the data
        _mockClient.Setup(client => client.GetNeoFeedAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync(fakeData);

        // Act: Call the method
        var result = await _service.GetTopHazardousAsteroidsAsync(1);

        // Assert: The result should be an empty list
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    /// <summary>
    /// Helper method to create a simulated hazardous asteroid.
    /// </summary>
    private static Asteroid CreateHazardousAsteroid(string name, double minDiameter, double maxDiameter, string date, string planet, string speed)
    {
        return new Asteroid
        {
            Name = name,
            IsPotentiallyHazardousAsteroid = true,
            EstimatedDiameter = new EstimatedDiameter
            {
                Kilometers = new DiameterKilometers
                {
                    EstimatedDiameterMin = minDiameter,
                    EstimatedDiameterMax = maxDiameter
                }
            },
            CloseApproachData =
            [
                new CloseApproachData
                {
                    CloseApproachDate = date,
                    OrbitingBody = planet,
                    RelativeVelocity = new RelativeVelocity
                    {
                        KilometersPerHour = speed
                    }
                }
            ]
        };
    }
}
