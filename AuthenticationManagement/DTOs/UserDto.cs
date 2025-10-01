namespace AuthenticationManagement.DTOs
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Role info
        public int RoleID { get; set; }
        public string RoleType { get; set; } = default!;
    }

    public class CreateUserDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public int RoleID { get; set; }
    }

    public class UpdateUserDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? Status { get; set; }
        public int RoleID { get; set; }
    }
}
