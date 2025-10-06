using Microsoft.EntityFrameworkCore;
using UserManagement.Models;
namespace UserManagement.SeedData
{
    public static class SkillSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
        //    modelBuilder.Entity<Skill>().HasData(
        //        new Skill
        //        {
        //            SkillId = 1,
        //            SkillName = "C#",
        //            SkillLevel = "Intermediate",
        //            MonthsOfExperience = 10,
        //            UserId = 1
        //        },
        //        new Skill
        //        {
        //            SkillId = 2,
        //            SkillName = "SQL",
        //            SkillLevel = "Advanced",
        //            MonthsOfExperience = 18,
        //            UserId = 1
        //        },
        //        new Skill
        //        {
        //            SkillId = 3,
        //            SkillName = "React",
        //            SkillLevel = "Beginner",
        //            MonthsOfExperience = 4,
        //            UserId = 2
        //        },
        //        new Skill
        //        {
        //            SkillId = 4,
        //            SkillName = "Python",
        //            SkillLevel = "Intermediate",
        //            MonthsOfExperience = 8,
        //            UserId = 3
        //        },
        //        new Skill
        //        {
        //            SkillId = 5,
        //            SkillName = "HTML & CSS",
        //            SkillLevel = "Advanced",
        //            MonthsOfExperience = 24,
        //            UserId = 2
        //        },
        //        new Skill
        //        {
        //            SkillId = 6,
        //            SkillName = "ASP.NET Core",
        //            SkillLevel = "Intermediate",
        //            MonthsOfExperience = 12,
        //            UserId = 1
        //        },
        //        new Skill
        //        {
        //            SkillId = 7,
        //            SkillName = "JavaScript",
        //            SkillLevel = "Intermediate",
        //            MonthsOfExperience = 15,
        //            UserId = 3
        //        }
        //    );
        }
    }
}