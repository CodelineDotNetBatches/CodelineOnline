using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.SeedData
{
    public static class TraineeSeedData
    {
        public static async Task SeedAsync(UsersDbContext context)
        {
            if (await context.Trainees.AnyAsync()) return;

            var batches = await context.Batches.ToListAsync();
            if (!batches.Any()) return; // Ensure Batch seed runs first

            var batch1 = batches.First();
            var batch2 = batches.Last();

            var trainees = new List<Trainee>
            {
                new Trainee
                {
                    TraineeId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    GithubUsername = "ali-dev",
                    Years_of_Experience = 2,
                    TraineeCV = "ali_cv.pdf",
                    Experience_Level = ExperienceLevel.Junior,
                    Learning_Style = LearningStyle.ProjectBased,
                    Study_Focus = StudyFocus.CSharp,
                    EducationalBackground = "BSc Computer Science",
                    LearningObjectives = "Become a full-stack .NET developer",
                    ProfileImage = "ali.png"
                },
                new Trainee
                {
                    TraineeId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    GithubUsername = "sara-code",
                    Years_of_Experience = 1,
                    TraineeCV = "sara_cv.pdf",
                    Experience_Level = ExperienceLevel.Junior,
                    Learning_Style = LearningStyle.SelfPaced,
                    Study_Focus = StudyFocus.Python,
                    EducationalBackground = "BSc IT",
                    LearningObjectives = "Enhance Python and data analysis skills",
                    ProfileImage = "sara.png"
                },
                new Trainee
                {
                    TraineeId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    GithubUsername = "mohammed-tech",
                    Years_of_Experience = 3,
                    TraineeCV = "mohammed_cv.pdf",
                    Experience_Level = ExperienceLevel.Mid,
                    Learning_Style = LearningStyle.Lecture,
                    Study_Focus = StudyFocus.JavaScript,
                    EducationalBackground = "Diploma in Software Engineering",
                    LearningObjectives = "Master web development and APIs",
                    ProfileImage = "mohammed.png"
                }
            };

            await context.Trainees.AddRangeAsync(trainees);
            await context.SaveChangesAsync();
        }
    }
}
