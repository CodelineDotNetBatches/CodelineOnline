using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder mb)
        {
            // ======================
            // Seed Data
            // ======================
            var prog1 = new Programs
            {
                ProgramId = Guid.NewGuid(),
                ProgramName = "Software Engineering",
                ProgramDescription = "Full stack software engineering program.",
                Roadmap = "Backend → Frontend → DevOps",
                CreatedAt = DateTime.UtcNow
            };
            var prog2 = new Programs
            {
                ProgramId = Guid.NewGuid(),
                ProgramName = "Data Science",
                ProgramDescription = "Learn Python, statistics, and ML models.",
                Roadmap = "Python → Statistics → ML → Deployment",
                CreatedAt = DateTime.UtcNow
            };
            var cat1 = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "Backend Development",
                CategoryDescription = "C#, ASP.NET Core, SQL Server, APIs",
                CreatedAt = DateTime.UtcNow
            };
            var cat2 = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = "Data Analytics",
                CategoryDescription = "Data visualization and analytics tools.",
                CreatedAt = DateTime.UtcNow
            };
            var course1 = new Course
            {
                CourseId = Guid.NewGuid(),
                CourseName = "ASP.NET Core Fundamentals",
                CourseDescription = "Learn how to build REST APIs using .NET Core.",
                CourseLevel = LevelType.Intermediate,
               
                CategoryId = cat1.CategoryId,
                CreatedAt = DateTime.UtcNow
            };
            var course2 = new Course
            {
                CourseId = Guid.NewGuid(),
                CourseName = "SQL Server & EF Core",
                CourseDescription = "Database management with SQL Server and EF Core ORM.",
                CourseLevel = LevelType.Beginner,
               
                CategoryId = cat1.CategoryId,
                CreatedAt = DateTime.UtcNow
            };
            var course3 = new Course
            {
                CourseId = Guid.NewGuid(),
                CourseName = "Machine Learning 101",
                CourseDescription = "Introduction to ML algorithms and data preprocessing.",
                CourseLevel = LevelType.Intermediate,
                
                CategoryId = cat2.CategoryId,
                CreatedAt = DateTime.UtcNow
            };
        }

    }
}

