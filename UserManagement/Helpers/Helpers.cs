namespace UserManagement.Helpers
{
    // Generic response wrapper for consistent API output.
    // Used by Controllers and Services.
    public class Helpers
    {
        public class ApiResponse<T>
        {
            public bool Success { get; set; } //true if operation succeeded
            public string Message { get; set; } //any message (error/success)
        }
}
