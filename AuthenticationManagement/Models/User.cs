using System.ComponentModel.DataAnnotations;

namespace AuthenticationManagement.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = default!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = default!;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }

        public string? Photo { get; set; }

        [MaxLength(20)]
        public string? NationalID { get; set; }

        [Required, MaxLength(150)]
        public string Email { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;

        [MaxLength(20)]
        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public int RoleID { get; set; }
        public Role Role { get; set; } = default!;
    }
}
