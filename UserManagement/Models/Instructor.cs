using System.ComponentModel.DataAnnotations.Schema;
namespace UserManagement.Models
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        public string GithubUserName { get; set; }
        public int Years_of_Experience { get; set; }
        public string ProfileImage { get; set; }
        public string InstructorCV { get; set; }
        public string Experience_Level { get; set; }
        public string Teaching_Style { get; set; }



        // navigation property
        public ICollection<Availability> Availabilities { get; set; }
    }
}
