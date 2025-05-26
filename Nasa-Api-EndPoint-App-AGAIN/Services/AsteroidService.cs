using System.Globalization;
using Nasa_Api_EndPoint_App_AGAIN.Client;
using Nasa_Api_EndPoint_App_AGAIN.Dto;
using Nasa_Api_EndPoint_App_AGAIN.Services;
using Nasa_Api_EndPoint_App_AGAIN.Util;

public class AsteroidService(INasaApiClient nasaApiClient) : IAsteroidService
{
    public async Task<IEnumerable<AsteroidDto>?> GetTopHazardousAsteroidsAsync(int days)
    {
        var startDate = DateTime.UtcNow.Date;
        var endDate = startDate.AddDays(days);
        var data = await nasaApiClient.GetNeoFeedAsync(startDate, endDate);

        var asteroids = data?.NearEarthObjects
            .SelectMany(x => x.Value)
            .Where(a => a.IsPotentiallyHazardousAsteroid)
            .Select(a =>
            {
                var approach = a.CloseApproachData.FirstOrDefault();
                var diameter = DiameterCalculator.GetAverageKm(a.EstimatedDiameter.Kilometers);

                return new AsteroidDto
                {
                    Name = a.Name,
                    Diameter = diameter,
                    Speed = double.Parse(approach?.RelativeVelocity.KilometersPerHour ?? "0", CultureInfo.InvariantCulture),
                    Date = approach?.CloseApproachDate,
                    Planet = approach?.OrbitingBody
                };
            })
            .OrderByDescending(a => a.Diameter)
            .Take(3)
            .ToList();

        return asteroids;
    }
}