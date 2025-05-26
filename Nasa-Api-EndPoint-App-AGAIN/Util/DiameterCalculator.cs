using Nasa_Api_EndPoint_App_AGAIN.Dto;

namespace Nasa_Api_EndPoint_App_AGAIN.Util;

public static class DiameterCalculator
{
    public static double GetAverageKm(DiameterKilometers d) =>
        (d.EstimatedDiameterMin + d.EstimatedDiameterMax) / 2;
}