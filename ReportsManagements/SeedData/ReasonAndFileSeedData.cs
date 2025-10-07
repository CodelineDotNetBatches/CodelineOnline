//using Microsoft.EntityFrameworkCore;
//using ReportsManagements.Models;



//namespace ReportsManagements.SeedData
//{
//    public class ReasonAndFileSeedData
//    {
//        public static ReportsDbContext CreateInMemoryDbContext()
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<ReportsDbContext>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

//            var context = new ReportsDbContext(optionsBuilder.Options);

//            // Seed ReasonCodes
//            context.ReasonCodes.AddRange(
//                new ReasonCode { ReasonCodeId = 1, Code = "LATE", Name = "Late", Category = "Attendance", IsActive = true },
//                new ReasonCode { ReasonCodeId = 2, Code = "SICK", Name = "Sick", Category = "Health", IsActive = true },
//                new ReasonCode { ReasonCodeId = 3, Code = "TECH", Name = "Technical Issue", Category = "System", IsActive = true }
//            );

//            // Seed FileStorage
//            context.FileStorages.AddRange(
//                new FileStorage
//                {
//                    FileStorageId = 1,
//                    FileName = "report1.pdf",
//                    Url = "https://dummy-url.com/report1.pdf",
//                    UploadedAt = DateTime.UtcNow,
//                    UploadedBy = "Admin"
//                },
//                new FileStorage
//                {
//                    FileStorageId = 2,
//                    FileName = "report2.pdf",
//                    Url = "https://dummy-url.com/report2.pdf",
//                    UploadedAt = DateTime.UtcNow,
//                    UploadedBy = "Admin"
//                }
//            );

//            context.SaveChanges();
//            return context;
//        }
//    }
//}

