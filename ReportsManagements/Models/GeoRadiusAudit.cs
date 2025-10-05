namespace ReportsManagements.Models
{
    public class GeoRadiusAudit
    {
        public int GeoRadiusAuditId { get; set; }
        public int GeolocationId { get; set; }
        public decimal OldRadius { get; set; }
        public decimal NewRadius { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
