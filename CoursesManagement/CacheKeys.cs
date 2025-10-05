namespace CoursesManagement.Caching
{
    /// <summary>
    /// Centralized cache key definitions for all entities in the Courses Management system.
    /// Use these to keep cache key naming consistent across the application.
    /// </summary>
    public static class CacheKeys
    {
        // ===========================
        // Course Cache Keys
        // ===========================

        /// <summary>
        /// Key for the list of all courses (used in GetAllCourses).
        /// </summary>
        public const string AllCourses = "courses_all";

        /// <summary>
        /// Key for a single course by ID.
        /// </summary>
        public static string Course(Guid courseId) => $"course_{courseId}";

        /// <summary>
        /// Key for courses filtered by category.
        /// </summary>
        public static string CoursesByCategory(Guid categoryId) => $"courses_category_{categoryId}";

        /// <summary>
        /// Key for courses filtered by program.
        /// </summary>
        public static string CoursesByProgram(Guid programId) => $"courses_program_{programId}";


        // ===========================
        // Category Cache Keys
        // ===========================

        /// <summary>
        /// Key for the list of all categories.
        /// </summary>
        public const string AllCategories = "categories_all";

        /// <summary>
        /// Key for a single category by ID.
        /// </summary>
        public static string Category(Guid categoryId) => $"category_{categoryId}";


        // ===========================
        // Program Cache Keys
        // ===========================

        /// <summary>
        /// Key for the list of all programs.
        /// </summary>
        public const string AllPrograms = "programs_all";

        /// <summary>
        /// Key for a single program by ID.
        /// </summary>
        public static string Program(Guid programId) => $"program_{programId}";


        // ===========================
        // Enrollment Cache Keys
        // ===========================

        /// <summary>
        /// Key for all enrollments (rarely cached fully, but defined for flexibility).
        /// </summary>
        public const string AllEnrollments = "enrollments_all";

        /// <summary>
        /// Key for a user's enrollments.
        /// </summary>
        public static string EnrollmentsByUser(Guid userId) => $"enrollments_user_{userId}";

        /// <summary>
        /// Key for enrollments by course.
        /// </summary>
        public static string EnrollmentsByCourse(Guid courseId) => $"enrollments_course_{courseId}";


        // ===========================
        // Certificate Cache Keys
        // ===========================

        /// <summary>
        /// Key for all certificates.
        /// </summary>
        public const string AllCertificates = "certificates_all";

        /// <summary>
        /// Key for certificates of a specific user.
        /// </summary>
        public static string CertificatesByUser(int userId) => $"certificates_user_{userId}";

        /// <summary>
        /// Key for a specific certificate by ID.
        /// </summary>
        public static string Certificate(int certificateId) => $"certificate_{certificateId}";


        // ===========================
        // Helper Method (Optional)
        // ===========================

        /// <summary>
        /// Generates a composite key for custom scenarios (e.g. search + pagination).
        /// </summary>
        //public static string Custom(string baseKey, params object[] values)
        //{
        //    var suffix = string.Join("_", values.Select(v => v?.ToString() ?? "null"));
        //    return $"{baseKey}_{suffix}";
        //}
    }
}
