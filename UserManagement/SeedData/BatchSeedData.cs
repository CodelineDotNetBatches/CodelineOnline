using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.SeedData
{
    public static class BatchSeedData
    {
        public static async Task SeedAsync(UsersDbContext context)
        {
            if (await context.Batches.AnyAsync()) return;

            var batch1 = new Batch
            {
                BatchId = Guid.NewGuid(),
                Name = "Full Stack .NET Bootcamp",
                Status = "Ongoing",
                StartDate = new DateTime(2025, 1, 10),
                EndDate = new DateTime(2025, 4, 20),
                Timeline = "14 Weeks",
                Description = "Comprehensive training in C#, ASP.NET Core, and SQL Server."
            };

            var batch2 = new Batch
            {
                BatchId = Guid.NewGuid(),
                Name = "Python Data Science Track",
                Status = "Planned",
                StartDate = new DateTime(2025, 5, 1),
                EndDate = new DateTime(2025, 7, 31),
                Timeline = "12 Weeks",
                Description = "Data analysis and visualization using Python, Pandas, and Power BI."
            };

            await context.Batches.AddRangeAsync(batch1, batch2);
            await context.SaveChangesAsync();
        }
    }
}
