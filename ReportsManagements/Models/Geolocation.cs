using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsManagements.Models
{
    public class Geolocation
    {
        [Key]
        public int GeolocationId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal RediusMeters { get; set; }

        // Foreign Key
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
    }
}
