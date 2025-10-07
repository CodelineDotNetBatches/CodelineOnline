using System.ComponentModel.DataAnnotations;

namespace AuthenticationManagement.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
        //[MaxLength(50)]
        //public string RoleType { get; set; } = default!;

        [MaxLength(200)]
        public string? Description { get; set; }

        // Navigation: One Role has many Users
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
