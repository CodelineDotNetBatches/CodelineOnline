namespace ReportsManagements.DTOs
{
    public class GeoValidateRequest
    {
        public int GeolocationId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
