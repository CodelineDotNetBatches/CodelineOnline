using System.ComponentModel.DataAnnotations;

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
            public T? Data { get; set; } //returned object or list



            public ApiResponse(bool success, string message, T? data = default)
            {
                Success = success;
                Message = message;
                Data = data;
            }

            // Helper factory methods
            public static ApiResponse<T> Ok(T data, string msg = "Operation succeeded") =>
          new ApiResponse<T>(true, msg, data);

            public static ApiResponse<T> Fail(string msg) =>
                new ApiResponse<T>(false, msg);
        }


        // Validation helper to manually validate models

        public static class ValidationHelper
        {
            public static List<ValidationResult> ValidateObject(object obj)
            {
                var context = new ValidationContext(obj, null, null);
                var results = new List<ValidationResult>();
                Validator.TryValidateObject(obj, context, results, true);
                return results;
            }
        }
}
