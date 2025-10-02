using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.SeedData
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            // ✅ Use fixed GUIDs (instead of Guid.NewGuid)
            var batch1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var batch2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var trainee1Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var trainee2Id = Guid.Parse("44444444-4444-4444-4444-444444444444");


            // Seed Batches
            modelBuilder.Entity<Batch>().HasData(
                new Batch
                {
                    BatchId = batch1Id,
                    Name = "C# Beginners",
                    Status = "Active",
                    StartDate = new DateTime(2025, 01, 01),
                    EndDate = new DateTime(2025, 03, 01),
                    Timeline = "Mon-Wed-Fri",
                    Description = "Introductory batch for C#"
                },
                new Batch
                {
                    BatchId = batch2Id,
                    Name = "Advanced SQL",
                    Status = "Planned",
                    StartDate = new DateTime(2025, 02, 01),
                    EndDate = new DateTime(2025, 04, 01),
                    Timeline = "Tue-Thu",
                    Description = "Deep dive into SQL optimization"
                }
            );

            // Seed Trainees
            modelBuilder.Entity<Trainee>().HasData(
                new Trainee
                {
                    TraineeId = trainee1Id,
                    GithubUsername = "ahmed-dev",
                    ProfileImage = "ahmed.png",
                    EducationalBackground = "BSc Computer Science",
                    TraineeCV = "ahmed_cv.pdf",
                    LearningObjectives = "Learn backend development",
                    ExperienceLevel = "Beginner"
                },
                new Trainee
                {
                    TraineeId = trainee2Id,
                    GithubUsername = "fatima-coder",
                    ProfileImage = "fatima.png",
                    EducationalBackground = "MSc Data Science",
                    TraineeCV = "fatima_cv.pdf",
                    LearningObjectives = "Advance SQL skills",
                    ExperienceLevel = "Intermediate"
                }
            );

            // Seed BatchTrainee (join table)
            modelBuilder.Entity<BatchTrainee>().HasData(
                new { BatchId = batch1Id, TraineeId = trainee1Id },
                new { BatchId = batch1Id, TraineeId = trainee2Id },
                new { BatchId = batch2Id, TraineeId = trainee2Id }
            );
        }
    }
}

