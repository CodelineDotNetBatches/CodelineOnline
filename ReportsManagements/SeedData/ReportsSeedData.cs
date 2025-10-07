using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;
using System;
using System.Linq;

namespace ReportsManagements.SeedData
{
    public static class DatabaseSeeder
    {
        public static void Seed(ReportsDbContext context)
        {
            context.Database.EnsureCreated();

            // ===== Seed Branches =====
            if (!context.Branches.Any())
            {
                var hqBranch = new Branch { Name = "HQ", Address = "City Center", IsActive = true };
                var westBranch = new Branch { Name = "West Branch", Address = "West Road", IsActive = true };

                context.Branches.AddRange(hqBranch, westBranch);
                context.SaveChanges();

                // ===== Seed Geolocations =====
                if (!context.Geolocations.Any())
                {
                    var geoList = new[]
                    {
                        new Geolocation { Name = "HQ Main Gate", Latitude = "23.5859", Longitude = "58.4059", RediusMeters = 100, BranchId = hqBranch.BranchId },
                        new Geolocation { Name = "HQ Parking", Latitude = "23.5865", Longitude = "58.4065", RediusMeters = 50, BranchId = hqBranch.BranchId },
                        new Geolocation { Name = "HQ Backdoor", Latitude = "23.5870", Longitude = "58.4070", RediusMeters = 75, BranchId = hqBranch.BranchId },
                        new Geolocation { Name = "West Main", Latitude = "23.6000", Longitude = "58.4200", RediusMeters = 100, BranchId = westBranch.BranchId },
                        new Geolocation { Name = "West Parking", Latitude = "23.6010", Longitude = "58.4210", RediusMeters = 50, BranchId = westBranch.BranchId },
                        new Geolocation { Name = "West Backdoor", Latitude = "23.6020", Longitude = "58.4220", RediusMeters = 75, BranchId = westBranch.BranchId }
                    };

                    context.Geolocations.AddRange(geoList);
                    context.SaveChanges();
                }

                // ===== Seed BranchReports =====
                if (!context.BranchReports.Any())
                {
                    var branchReports = new[]
                    {
                        new BranchReport
                        {
                            BranchId = hqBranch.BranchId,
                            TotalSessions = 20,
                            TotalStudents = 150,
                            AttendanceRate = 95,
                            TotalInstructors = 10
                        },
                        new BranchReport
                        {
                            BranchId = westBranch.BranchId,
                            TotalSessions = 15,
                            TotalStudents = 120,
                            AttendanceRate = 90,
                            TotalInstructors = 8
                        }
                    };

                    context.BranchReports.AddRange(branchReports);
                    context.SaveChanges();
                }
            }

            // ===== Seed CourseReports =====
            if (!context.CourseReports.Any())
            {
                var courseReports = new[]
                {
                    new CourseReport
                    {
                        CourseId = 1,
                        TotalSessions = 12,
                        TotalStudents = 85,
                        AverageAttendanceRate = 92.5m
                    },
                    new CourseReport
                    {
                        CourseId = 2,
                        TotalSessions = 10,
                        TotalStudents = 60,
                        AverageAttendanceRate = 88.3m
                    },
                    new CourseReport
                    {
                        CourseId = 3,
                        TotalSessions = 8,
                        TotalStudents = 45,
                        AverageAttendanceRate = 94.1m
                    }
                };

                context.CourseReports.AddRange(courseReports);
                context.SaveChanges();
            }

            // ===== Seed TrainerReports =====
            if (!context.TrainerReports.Any())
            {
                var trainerReports = new[]
                {
                    new TrainerReport
                    {
                        TrainerId = 1,
                        TotalSessions = 20,
                        TotalStudents = 120,
                        AttendanceRate = 94.5m,
                        
                    },
                    new TrainerReport
                    {
                        TrainerId = 2,
                        TotalSessions = 15,
                        TotalStudents = 100,
                        AttendanceRate = 91.2m,
                       
                    },
                    new TrainerReport
                    {
                        TrainerId = 3,
                        TotalSessions = 18,
                        TotalStudents = 110,
                        AttendanceRate = 96.8m,
                        
                    }
                };

                context.TrainerReports.AddRange(trainerReports);
                context.SaveChanges();
            }

            // ===== Seed ReasonCodes =====
            if (!context.ReasonCodes.Any())
            {
                context.ReasonCodes.AddRange(
                    new ReasonCode { Code = "LATE", Name = "Late", Category = "Attendance", IsActive = true },
                    new ReasonCode { Code = "SICK", Name = "Sick", Category = "Health", IsActive = true },
                    new ReasonCode { Code = "TECH", Name = "Technical Issue", Category = "System", IsActive = true }
                );
                context.SaveChanges();
            }

            // ===== Seed FileStorage =====
            if (!context.FileStorages.Any())
            {
                context.FileStorages.AddRange(
                    new FileStorage
                    {
                        FileName = "report1.pdf",
                        Url = "https://dummy-url.com/report1.pdf",
                        UploadedAt = DateTime.UtcNow,
                        UploadedBy = "Admin"
                    },
                    new FileStorage
                    {
                        FileName = "report2.pdf",
                        Url = "https://dummy-url.com/report2.pdf",
                        UploadedAt = DateTime.UtcNow,
                        UploadedBy = "Admin"
                    }
                );
                context.SaveChanges();
            }

            // ===== Seed AttendanceRecords =====
            if (!context.AttendanceRecord.Any())
            {
                var geos = context.Geolocations.Take(2).ToList();
                var geo1 = geos.ElementAtOrDefault(0);
                var geo2 = geos.ElementAtOrDefault(1);
                var reason = context.ReasonCodes.FirstOrDefault();

                if (geo1 != null && geo2 != null)
                {
                    context.AttendanceRecord.AddRange(
                        new AttendanceRecord
                        {
                            SessionId = 101,
                            StudentId = 1001,
                            CheckIn = DateTime.UtcNow.AddMinutes(-45),
                            CheckOut = DateTime.UtcNow,
                            Status = "Present",
                            ReviewStatus = "Approved",
                            GeolocationId = geo1.GeolocationId,
                            FaceMatchScore = 0.95,
                            LivenessScore = 0.98,
                            CreatedBy = "Seeder",
                            CreatedAt = DateTime.UtcNow,
                            UploadedBy = "Admin",
                            UploadedAt = DateTime.UtcNow,
                            IdempotencyKey = Guid.NewGuid().ToString()
                        },
                        new AttendanceRecord
                        {
                            SessionId = 101,
                            StudentId = 1002,
                            CheckIn = DateTime.UtcNow.AddMinutes(-30),
                            Status = "Late",
                            ReviewStatus = "Pending",
                            ReasonCodeId = reason?.ReasonCodeId,
                            GeolocationId = geo2.GeolocationId,
                            FaceMatchScore = 0.80,
                            LivenessScore = 0.75,
                            CreatedBy = "Seeder",
                            CreatedAt = DateTime.UtcNow,
                            UploadedBy = "Admin",
                            UploadedAt = DateTime.UtcNow,
                            IdempotencyKey = Guid.NewGuid().ToString()
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
