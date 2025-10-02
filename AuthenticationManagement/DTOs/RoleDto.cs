namespace AuthenticationManagement.DTOs
{
    public class RoleDto
    {
        public int RoleID { get; set; }
        public string RoleType { get; set; } = default!;
        public string? Description { get; set; }
    }

    public class CreateRoleDto
    {
        public string RoleType { get; set; } = default!;
        public string? Description { get; set; }
    }

    public class UpdateRoleDto
    {
        public string RoleType { get; set; } = default!;
        public string? Description { get; set; }
    }
}
