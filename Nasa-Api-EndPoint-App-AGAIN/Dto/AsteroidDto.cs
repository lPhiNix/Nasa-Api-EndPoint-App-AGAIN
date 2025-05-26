namespace Nasa_Api_EndPoint_App_AGAIN.Dto
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a hazardous asteroid.
    /// </summary>
    /// <remarks>
    /// This class is used to decouple the internal domain model from the external data format returned by the API.
    /// It acts as a projection of relevant asteroid information for consumption by the API clients (frontend, other systems, etc.).
    ///
    /// Using DTOs ensures:
    /// - Simplified response structures
    /// - Security by exposing only necessary fields
    /// - Flexibility to reshape data independently of the underlying source models
    /// </remarks>
    public class AsteroidDto
    {
        /// <summary>
        /// The official name of the asteroid.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// The average diameter of the asteroid in kilometers.
        /// </summary>
        /// <remarks>
        /// This is computed from the min and max values returned by NASA’s API.
        /// It is marked as init-only because once calculated, it should not change.
        /// </remarks>
        public required double Diameter { get; init; }

        /// <summary>
        /// The relative speed of the asteroid in kilometers per hour.
        /// </summary>
        /// <remarks>
        /// Pulled from NASA’s "close_approach_data.relative_velocity" field.
        /// </remarks>
        public required double Speed { get; set; }

        /// <summary>
        /// The date on which the asteroid is predicted to make its closest approach to Earth (or another body).
        /// </summary>
        /// <remarks>
        /// This is a string in "yyyy-MM-dd" format, as returned by NASA’s API.
        /// Nullable in case the approach data is missing.
        /// </remarks>
        public required string? Date { get; set; }

        /// <summary>
        /// The celestial body that the asteroid will orbit or pass near (e.g., Earth, Mars).
        /// </summary>
        /// <remarks>
        /// Retrieved from the "orbiting_body" field of the API. Nullable for consistency with NASA data format.
        /// </remarks>
        public required string? Planet { get; set; }
    }
}
