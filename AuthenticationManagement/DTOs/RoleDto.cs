namespace AuthenticationManagement.DTOs
{
    public class RoleDto
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = default!;
        public string? Description { get; set; }
    }

    public class CreateRoleDto
    {
        public string RoleName { get; set; } = default!;
        public string? Description { get; set; }
    }

    public class UpdateRoleDto
    {
        public string RoleName { get; set; } = default!;
        public string? Description { get; set; }
    }
}
