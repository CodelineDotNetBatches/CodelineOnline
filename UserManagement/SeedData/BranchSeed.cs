using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.SeedData
{
    public static class BranchSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    BranchId = 1,
                    BranchName = "CodeLine_Muscat",
                    City = "Muscat",
                    Country = "Oman",
                    Email = "muscat@trainingcenter.com",
                    IsActive = true,
                },
                new Branch
                {
                    BranchId = 2,
                    BranchName = "CodeLine_Suwaiq",
                    City = "Suwaiq",
                    Country = "Oman",
                    Email = "salalah@trainingcenter.com",
                    IsActive = true,
                }
               
            );
        }
    }
}
