using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder mb)
        {
            // ======================
            // Static IDs to ensure relationships remain valid
            // ======================
            var prog1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var prog2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var cat1Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var cat2Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var course1Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var course2Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var course3Id = Guid.Parse("77777777-7777-7777-7777-777777777777");

            // Optional user IDs for demo enrollments
            var user1Id = Guid.Parse("99999999-9999-9999-9999-999999999999");
            var user2Id = Guid.Parse("88888888-8888-8888-8888-888888888888");
            var user3Id = Guid.Parse("77777777-7777-4444-8888-111111111111");

            // ======================
            // PROGRAMS
            // ======================
            var prog1 = new Programs
            {
                ProgramId = prog1Id,
                ProgramName = "Software Engineering",
                ProgramDescription = "Full stack software engineering program.",
                Roadmap = "Backend → Frontend → DevOps"
            };
            var prog2 = new Programs
            {
                ProgramId = prog2Id,
                ProgramName = "Data Science",
                ProgramDescription = "Learn Python, statistics, and ML models.",
                Roadmap = "Python → Statistics → ML → Deployment"
            };

            // ======================
            // CATEGORIES
            // ======================
            var cat1 = new Category
            {
                CategoryId = cat1Id,
                CategoryName = "Backend Development",
                CategoryDescription = "C#, ASP.NET Core, SQL Server, APIs"
            };
            var cat2 = new Category
            {
                CategoryId = cat2Id,
                CategoryName = "Data Analytics",
                CategoryDescription = "Data visualization and analytics tools."
            };

            // ======================
            // COURSES
            // ======================
            var course1 = new Course
            {
                CourseId = course1Id,
                CourseName = "ASP.NET Core Fundamentals",
                CourseDescription = "Learn how to build REST APIs using .NET Core.",
                CourseLevel = LevelType.Intermediate,
                CategoryId = cat1Id
            };
            var course2 = new Course
            {
                CourseId = course2Id,
                CourseName = "SQL Server & EF Core",
                CourseDescription = "Database management with SQL Server and EF Core ORM.",
                CourseLevel = LevelType.Beginner,
                CategoryId = cat1Id
            };
            var course3 = new Course
            {
                CourseId = course3Id,
                CourseName = "Machine Learning 101",
                CourseDescription = "Introduction to ML algorithms and data preprocessing.",
                CourseLevel = LevelType.Intermediate,
                CategoryId = cat2Id
            };

            // ======================
            // ENROLLMENTS
            // ======================
            var enrollment1 = new Enrollment
            {
                EnrollmentId = Guid.Parse("AAAA1111-AAAA-AAAA-AAAA-AAAAAAAAAAAA"),
                UserId = user1Id,
                CourseId = course1Id,
                ProgramId = prog1Id,
                EnrolledAt = DateTime.UtcNow.AddDays(-20),
                Status = "Active",
                Grade = null,
                StatusChangeReason = null
            };

            var enrollment2 = new Enrollment
            {
                EnrollmentId = Guid.Parse("BBBB2222-BBBB-BBBB-BBBB-BBBBBBBBBBBB"),
                UserId = user2Id,
                CourseId = course2Id,
                ProgramId = prog1Id,
                EnrolledAt = DateTime.UtcNow.AddDays(-45),
                Status = "Completed",
                Grade = 91.5m,
                StatusChangeReason = "Finished successfully"
            };

            var enrollment3 = new Enrollment
            {
                EnrollmentId = Guid.Parse("CCCC3333-CCCC-CCCC-CCCC-CCCCCCCCCCCC"),
                UserId = user3Id,
                CourseId = course3Id,
                ProgramId = prog2Id,
                EnrolledAt = DateTime.UtcNow.AddDays(-10),
                Status = "Active",
                Grade = null,
                StatusChangeReason = null
            };

            var enrollment4 = new Enrollment
            {
                EnrollmentId = Guid.Parse("DDDD4444-DDDD-DDDD-DDDD-DDDDDDDDDDDD"),
                UserId = user1Id,
                CourseId = course2Id,
                ProgramId = prog1Id,
                EnrolledAt = DateTime.UtcNow.AddDays(-5),
                Status = "Dropped",
                Grade = null,
                StatusChangeReason = "Dropped due to absence"
            };

            // ======================
            // Apply Seed Data
            // ======================
            mb.Entity<Programs>().HasData(prog1, prog2);
            mb.Entity<Category>().HasData(cat1, cat2);
            mb.Entity<Course>().HasData(course1, course2, course3);
            mb.Entity<Enrollment>().HasData(enrollment1, enrollment2, enrollment3, enrollment4);

            mb.Entity("ProgramCategories").HasData(
                new
                {
                    ProgramsProgramId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CategoriesCategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333")
                },
                new
                {
                    ProgramsProgramId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CategoriesCategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444")
                }
            );


            mb.Entity("ProgramCourses").HasData(
                new
                {
                    ProgramsProgramId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CoursesCourseId = Guid.Parse("55555555-5555-5555-5555-555555555555")
                },
                new
                {
                    ProgramsProgramId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CoursesCourseId = Guid.Parse("66666666-6666-6666-6666-666666666666")
                },
                new
                {
                    ProgramsProgramId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CoursesCourseId = Guid.Parse("77777777-7777-7777-7777-777777777777")
                }
            );
        }
    }
}
