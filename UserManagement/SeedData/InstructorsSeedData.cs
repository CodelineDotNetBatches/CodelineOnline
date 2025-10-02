using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.SeedData
{
    public class InstructorsSeedData
    {
        public static void InstructorsSeed(ModelBuilder mb)
        {
            mb.Entity<Instructor>().HasData(
                new Instructor
                {
                    InstructorId = 101,
                    GithubUserName = "aliceGH",
                    Years_of_Experience = 5,
                    ProfileImage = "https://example.com/images/alice.jpg",
                    InstructorCV = "https://example.com/cv/alice.pdf",
                    Experience_Level = ExperienceLevel.Mid,
                    Teaching_Style = TeachingStyle.ProjectBased,
                    Specialization = Specializations.CSharp
                },
                new Instructor
                {
                    InstructorId = 102,
                    GithubUserName = "bobDev",
                    Years_of_Experience = 8,
                    ProfileImage = "https://example.com/images/bob.jpg",
                    InstructorCV = "https://example.com/cv/bob.pdf",
                    Experience_Level = ExperienceLevel.Senior,
                    Teaching_Style = TeachingStyle.TheoryFirst,
                    Specialization = Specializations.DataScience
                },
                new Instructor
                {
                    InstructorId = 103,
                    GithubUserName = "carolCode",
                    Years_of_Experience = 3,
                    ProfileImage = "https://example.com/images/carol.jpg",
                    InstructorCV = "https://example.com/cv/carol.pdf",
                    Experience_Level = ExperienceLevel.Junior,
                    Teaching_Style = TeachingStyle.HandsOn,
                    Specialization = Specializations.DevOps
                },
                new Instructor
                {
                    InstructorId = 104,
                    GithubUserName = "daveTech",
                    Years_of_Experience = 10,
                    ProfileImage = "https://example.com/images/dave.jpg",
                    InstructorCV = "https://example.com/cv/dave.pdf",
                    Experience_Level = ExperienceLevel.Senior,
                    Teaching_Style = TeachingStyle.Lecture,
                    Specialization = Specializations.WebDevelopment
                }
            );



        }

    }
}
