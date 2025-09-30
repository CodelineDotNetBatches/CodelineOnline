using System.ComponentModel.DataAnnotations;

namespace ReportsManagements.Models
{
    public class ReasonCode
    {
        [Key]
        public int ReasonCodeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }

    }
}
