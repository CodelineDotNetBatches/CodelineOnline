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

            public static bool IsValid(object obj, out List<ValidationResult> errors)
            {
                errors = ValidateObject(obj);
                return !errors.Any();
            }
        }


        // Logging helper to standardize error logs across the app

        public static class LogHelper
        {
            public static void LogError(ILogger logger, Exception ex, string contextInfo = "")
            {
                logger.LogError($"❌ ERROR: {contextInfo}\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }

            public static void LogInfo(ILogger logger, string message)
            {
                logger.LogInformation($"ℹ️ {message}");
            }

            public static void LogWarning(ILogger logger, string message)
            {
                logger.LogWarning($"⚠️ {message}");
            }
        }

        // Common utilities for time, strings, and IDs

        public static class UtilityHelper
        {
            // Generate a readable timestamp
            public static string GetTimestamp() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Generate a random alphanumeric code (for Admin IDs or tracking)
            public static string GenerateCode(int length = 8)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            // Capitalize first letter of each word
            public static string CapitalizeWords(string text)
            {
                if (string.IsNullOrWhiteSpace(text)) return text;
                return string.Join(" ", text.Split(' ')
                    .Where(w => !string.IsNullOrWhiteSpace(w))
                    .Select(w => char.ToUpper(w[0]) + w.Substring(1).ToLower()));
            }



        }

    }
}
