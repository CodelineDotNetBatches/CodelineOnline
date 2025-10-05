using System.ComponentModel.DataAnnotations;

namespace ReportsManagements.Models
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public bool IsActive { get; set; }= true;

        // Navigation property: A branch can have many geolocations
        public ICollection<Geolocation> Geolocations { get; set; } = new List<Geolocation>();
    }
}
