namespace Nasa_Api_EndPoint_App_AGAIN.Dto
{
    public class AsteroidDto
    {
        public required string Name { get; set; }
        public required double Diameter { get; init; }
        public required double Speed { get; set; }
        public required string Date { get; set; }
        public required string Planet { get; set; }
    }
}