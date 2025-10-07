using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder mb)
        {
            // ======================
            // Static IDs to avoid EF Core dynamic model warnings
            // ======================
            var prog1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var prog2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var cat1Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var cat2Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var course1Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var course2Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var course3Id = Guid.Parse("77777777-7777-7777-7777-777777777777");

            // ======================
            // Seed Data
            // ======================
            var prog1 = new Programs
            {
                ProgramId = prog1Id,
                ProgramName = "Software Engineering",
                ProgramDescription = "Full stack software engineering program.",
                Roadmap = "Backend → Frontend → DevOps",
                //CreatedAt = DateTime.UtcNow
            };
            var prog2 = new Programs
            {
                ProgramId = Guid.NewGuid(),
                ProgramName = "Data Science",
                ProgramDescription = "Learn Python, statistics, and ML models.",
                Roadmap = "Python → Statistics → ML → Deployment",
                //CreatedAt = DateTime.UtcNow
            };
            var cat1 = new Category
            {
                CategoryId = cat1Id,
                CategoryName = "Backend Development",
                CategoryDescription = "C#, ASP.NET Core, SQL Server, APIs",
                //CreatedAt = DateTime.UtcNow
            };
            var cat2 = new Category
            {
                CategoryId = cat2Id,
                CategoryName = "Data Analytics",
                CategoryDescription = "Data visualization and analytics tools.",
                //CreatedAt = DateTime.UtcNow
            };
            var course1 = new Course
            {
                CourseId = course1Id,   
                CourseName = "ASP.NET Core Fundamentals",
                CourseDescription = "Learn how to build REST APIs using .NET Core.",
                CourseLevel = LevelType.Intermediate,
               
                CategoryId = cat1.CategoryId,
                //CreatedAt = DateTime.UtcNow
            };
            var course2 = new Course
            {
                CourseId = course2Id,
                CourseName = "SQL Server & EF Core",
                CourseDescription = "Database management with SQL Server and EF Core ORM.",
                CourseLevel = LevelType.Beginner,

                CategoryId = cat1.CategoryId,
                //CreatedAt = DateTime.UtcNow
            };
            var course3 = new Course
            {
                CourseId = course3Id,
                CourseName = "Machine Learning 101",
                CourseDescription = "Introduction to ML algorithms and data preprocessing.",
                CourseLevel = LevelType.Intermediate,
                
                CategoryId = cat2.CategoryId,
                //CreatedAt = DateTime.UtcNow
            };

            //to add the objects to the database ...
            mb.Entity<Programs>().HasData(prog1, prog2);
            mb.Entity<Category>().HasData(cat1, cat2);
            mb.Entity<Course>().HasData(course1, course2, course3);
        }

    }
}

