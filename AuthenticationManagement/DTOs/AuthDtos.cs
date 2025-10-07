namespace AuthenticationManagement.DTOs
{
    public class RegisterDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? RoleName { get; set; } = "User"; // Default rolepublic string Password { get; set; } = default!;
       // public int RoleID { get; set; }  // e.g., default=User role
    }

    public class LoginDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class AuthResponseDto
    {
        public int UserID { get; set; }
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime ExpiresAtUtc { get; set; }
    }
}
