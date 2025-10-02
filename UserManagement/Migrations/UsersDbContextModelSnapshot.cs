using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserManagement;

#nullable disable

namespace UserManagement.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    partial class UsersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("users")
                .HasAnnotation("ProductVersion", "9.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserManagement.Models.Availability", b =>
                {
                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<int>("avilabilityId")
                        .HasColumnType("int");

                    b.Property<int>("Avail_Status")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<int>("day_of_week")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<TimeOnly>("time")
                        .HasColumnType("time");

                    b.HasKey("InstructorId", "avilabilityId");

                    b.HasIndex("Avail_Status");

                    b.ToTable("Availabilities", "users");
            modelBuilder.Entity("UserManagement.Models.Batch", b =>
                {
                    b.Property<Guid>("BatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Timeline")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BatchId");

                    b.ToTable("Batches", "users");

                    b.HasData(
                        new
                        {
                            InstructorId = 101,
                            avilabilityId = 1,
                            Avail_Status = 1,
                            day_of_week = 1,
                            time = new TimeOnly(9, 0, 0)
                        },
                        new
                        {
                            InstructorId = 101,
                            avilabilityId = 2,
                            Avail_Status = 3,
                            day_of_week = 3,
                            time = new TimeOnly(14, 0, 0)
                        },
                        new
                        {
                            InstructorId = 102,
                            avilabilityId = 3,
                            Avail_Status = 2,
                            day_of_week = 2,
                            time = new TimeOnly(11, 0, 0)
                        },
                        new
                        {
                            InstructorId = 102,
                            avilabilityId = 4,
                            Avail_Status = 4,
                            day_of_week = 4,
                            time = new TimeOnly(16, 0, 0)
                        },
                        new
                        {
                            InstructorId = 103,
                            avilabilityId = 5,
                            Avail_Status = 1,
                            day_of_week = 5,
                            time = new TimeOnly(10, 0, 0)
                        },
                        new
                        {
                            InstructorId = 104,
                            avilabilityId = 6,
                            Avail_Status = 3,
                            day_of_week = 6,
                            time = new TimeOnly(13, 0, 0)
                        });
                });

            modelBuilder.Entity("UserManagement.Models.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<int>("Experience_Level")
                        .HasMaxLength(30)
                        .HasColumnType("int");

                    b.Property<string>("GithubUserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("InstructorCV")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ProfileImage")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Specialization")
                        .HasMaxLength(80)
                        .HasColumnType("int");

                    b.Property<int>("Teaching_Style")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("Years_of_Experience")
                        .HasColumnType("int");

                    b.HasKey("InstructorId");

                    b.HasIndex("GithubUserName")
                        .IsUnique();

                    b.HasIndex("Specialization");

                    b.ToTable("Instructors", "users");
=======
                            BatchId = new Guid("11111111-1111-1111-1111-111111111111"),
                            Description = "Introductory batch for C#",
                            EndDate = new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "C# Beginners",
                            StartDate = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Active",
                            Timeline = "Mon-Wed-Fri"
                        },
                        new
                        {
                            BatchId = new Guid("22222222-2222-2222-2222-222222222222"),
                            Description = "Deep dive into SQL optimization",
                            EndDate = new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Advanced SQL",
                            StartDate = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Planned",
                            Timeline = "Tue-Thu"
                        });
                });

            modelBuilder.Entity("UserManagement.Models.BatchTrainee", b =>
                {
                    b.Property<Guid>("BatchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TraineeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BatchId", "TraineeId");

                    b.HasIndex("TraineeId");

                    b.ToTable("BatchTrainees", "users");

                    b.HasData(
                        new
                        {
                            BatchId = new Guid("11111111-1111-1111-1111-111111111111"),
                            TraineeId = new Guid("33333333-3333-3333-3333-333333333333")
                        },
                        new
                        {
                            BatchId = new Guid("11111111-1111-1111-1111-111111111111"),
                            TraineeId = new Guid("44444444-4444-4444-4444-444444444444")
                        },
                        new
                        {
                            BatchId = new Guid("22222222-2222-2222-2222-222222222222"),
                            TraineeId = new Guid("44444444-4444-4444-4444-444444444444")
                        });
                });

            modelBuilder.Entity("UserManagement.Models.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillId"));

                    b.Property<int>("MonthsOfExperience")
                        .HasColumnType("int");

                    b.Property<int>("SkillLevel")
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkillId");

                    b.ToTable("Skills", "users");
                });

            modelBuilder.Entity("UserManagement.Models.Trainee", b =>
                {
                    b.Property<Guid>("TraineeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EducationalBackground")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExperienceLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GithubUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LearningObjectives")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TraineeCV")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TraineeId");

                    b.ToTable("Trainees", "users");

                    b.HasData(
                        new
                        {
                            InstructorId = 101,
                            Experience_Level = 1,
                            GithubUserName = "aliceGH",
                            InstructorCV = "https://example.com/cv/alice.pdf",
                            ProfileImage = "https://example.com/images/alice.jpg",
                            Specialization = 0,
                            Teaching_Style = 0,
                            Years_of_Experience = 5
                        },
                        new
                        {
                            InstructorId = 102,
                            Experience_Level = 2,
                            GithubUserName = "bobDev",
                            InstructorCV = "https://example.com/cv/bob.pdf",
                            ProfileImage = "https://example.com/images/bob.jpg",
                            Specialization = 4,
                            Teaching_Style = 1,
                            Years_of_Experience = 8
                        },
                        new
                        {
                            InstructorId = 103,
                            Experience_Level = 0,
                            GithubUserName = "carolCode",
                            InstructorCV = "https://example.com/cv/carol.pdf",
                            ProfileImage = "https://example.com/images/carol.jpg",
                            Specialization = 5,
                            Teaching_Style = 2,
                            Years_of_Experience = 3
                        },
                        new
                        {
                            InstructorId = 104,
                            Experience_Level = 2,
                            GithubUserName = "daveTech",
                            InstructorCV = "https://example.com/cv/dave.pdf",
                            ProfileImage = "https://example.com/images/dave.jpg",
                            Specialization = 7,
                            Teaching_Style = 3,
                            Years_of_Experience = 10
                        });
                });

            modelBuilder.Entity("UserManagement.Models.Trainee", b =>
                {
                    b.Property<int>("UID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UID"));

                    b.HasKey("UID");

                    b.ToTable("Trainees", "users");
                });

            modelBuilder.Entity("UserManagement.Models.Availability", b =>
                {
                    b.HasOne("UserManagement.Models.Instructor", "Instructor")
                        .WithMany("Availabilities")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("UserManagement.Models.Instructor", b =>
                {
                    b.Navigation("Availabilities");

                            TraineeId = new Guid("33333333-3333-3333-3333-333333333333"),
                            EducationalBackground = "BSc Computer Science",
                            ExperienceLevel = "Beginner",
                            GithubUsername = "ahmed-dev",
                            LearningObjectives = "Learn backend development",
                            ProfileImage = "ahmed.png",
                            TraineeCV = "ahmed_cv.pdf"
                        },
                        new
                        {
                            TraineeId = new Guid("44444444-4444-4444-4444-444444444444"),
                            EducationalBackground = "MSc Data Science",
                            ExperienceLevel = "Intermediate",
                            GithubUsername = "fatima-coder",
                            LearningObjectives = "Advance SQL skills",
                            ProfileImage = "fatima.png",
                            TraineeCV = "fatima_cv.pdf"
                        });
                });

            modelBuilder.Entity("UserManagement.Models.TraineeSkill", b =>
                {
                    b.Property<Guid>("TraineeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("TraineeId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("TraineeSkills", "users");
                });

            modelBuilder.Entity("UserManagement.Models.BatchTrainee", b =>
                {
                    b.HasOne("UserManagement.Models.Batch", "Batch")
                        .WithMany("BatchTrainees")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserManagement.Models.Trainee", "Trainee")
                        .WithMany("BatchTrainees")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Batch");

                    b.Navigation("Trainee");
                });

            modelBuilder.Entity("UserManagement.Models.TraineeSkill", b =>
                {
                    b.HasOne("UserManagement.Models.Skill", "Skill")
                        .WithMany("TraineeSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserManagement.Models.Trainee", "Trainee")
                        .WithMany("TraineeSkills")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("Trainee");
                });

            modelBuilder.Entity("UserManagement.Models.Batch", b =>
                {
                    b.Navigation("BatchTrainees");
                });

            modelBuilder.Entity("UserManagement.Models.Skill", b =>
                {
                    b.Navigation("TraineeSkills");
                });

            modelBuilder.Entity("UserManagement.Models.Trainee", b =>
                {
                    b.Navigation("BatchTrainees");

                    b.Navigation("TraineeSkills");
                });
#pragma warning restore 612, 618
        }
    }
}
