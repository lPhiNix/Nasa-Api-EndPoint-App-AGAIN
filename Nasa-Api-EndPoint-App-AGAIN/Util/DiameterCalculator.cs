using Nasa_Api_EndPoint_App_AGAIN.Dto;

namespace Nasa_Api_EndPoint_App_AGAIN.Util;

/// <summary>
/// Utility class for diameter-related calculations.
/// </summary>
/// <remarks>
/// This static class provides helper methods to perform calculations related to asteroid diameters.
/// It centralizes the logic for computing average diameters from NASA API's min and max estimates.
/// 
/// This design choice promotes separation of concerns by isolating calculation logic away from domain models 
/// and service layers, making the code easier to maintain, test, and extend.
/// Since the calculation is stateless and purely functional, a static class is appropriate to avoid unnecessary instantiation.
/// </remarks>
public static class DiameterCalculator
{
    /// <summary>
    /// Calculates the average diameter in kilometers by averaging the minimum and maximum estimated diameters.
    /// </summary>
    /// <param name="d">DiameterKilometers object containing min and max diameter estimates.</param>
    /// <returns>Average diameter in kilometers as a double.</returns>
    public static double GetAverageKm(DiameterKilometers d) =>
        (d.EstimatedDiameterMin + d.EstimatedDiameterMax) / 2;
}