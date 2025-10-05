using ReportsManagements.Models;
using System.Linq;

namespace ReportsManagements.SeedData
{
    public static class DatabaseSeeder
    {
        public static void Seed(ReportsDbContext context)
        {
            if (!context.Geolocations.Any())
            {
                context.Geolocations.AddRange(
                    new Geolocation { Latitude = "23.6", Longitude = "58.5", IsActive = true, RediusMeters = 100 },
                    new Geolocation { Latitude = "23.7", Longitude = "58.6", IsActive = true, RediusMeters = 150 }
                );
                context.SaveChanges();
            }

            // Seed ReasonCodes
            if (!context.ReasonCodes.Any())
            {
                context.ReasonCodes.AddRange(
                    new ReasonCode { Code = "LATE", Name = "Late Arrival", Category = "Attendance", IsActive = true },
                    new ReasonCode { Code = "ABS", Name = "Absent", Category = "Attendance", IsActive = true }
                );
                context.SaveChanges(); 
            }

            // Seed FileStorage
            if (!context.FileStorages.Any())
            {
                context.FileStorages.AddRange(
                    new FileStorage { FileName = "photo1.jpg", Url = "/uploads/photo1.jpg", UploadedAt = DateTime.UtcNow, UploadedBy = "Seeder" },
                    new FileStorage { FileName = "photo2.jpg", Url = "/uploads/photo2.jpg", UploadedAt = DateTime.UtcNow, UploadedBy = "Seeder" }
                );
                context.SaveChanges();
            }
            var geos = context.Geolocations.Take(2).ToList();
            var geo1 = geos[0];
            var geo2 = geos[1];

            // Seed AttendanceRecords
            if (!context.AttendanceRecord.Any())
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
                      CreatedBy = "System",
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
                    ReasonCodeId = context.ReasonCodes.First().ReasonCodeId,
                     GeolocationId = geo2.GeolocationId,
                     FaceMatchScore = 0.80,
                     LivenessScore = 0.75,
                     CreatedBy = "System",
                     CreatedAt = DateTime.UtcNow,
                    UploadedBy = "Admin",
                    UploadedAt = DateTime.UtcNow,
                    IdempotencyKey = Guid.NewGuid().ToString() 

                      }
                         );

            }
        }
    }
}
