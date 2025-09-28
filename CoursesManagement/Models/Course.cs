using System.ComponentModel.DataAnnotations;

namespace CoursesManagement.Models
{
    public class Course
    {
        [Key]
        public int CID { get; set; }
    }
}
