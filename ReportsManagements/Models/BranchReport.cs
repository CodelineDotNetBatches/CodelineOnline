using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsManagements.Models
{
    public class BranchReport
    {
        [Key]
        public int BranchReportId { get; set; }
        public int TotalSessions { get; set; }
        public int TotalStudents { get; set; }
        public int AttendanceRate { get; set; }

        // Foreign Key
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
    }
}
