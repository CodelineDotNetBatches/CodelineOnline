using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.SeedData
{
    public static class AdminProfileSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Minimal seed (based on current model)
            modelBuilder.Entity<Admin_Profile>().HasData(
                new Admin_Profile
                {
                    AdminId = 1
                },
                new Admin_Profile
                {
                    AdminId = 2
                },
                new Admin_Profile
                {
                    AdminId = 3
                }
            );
        }
    }
}
