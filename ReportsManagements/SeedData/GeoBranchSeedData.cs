using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;

namespace ReportsManagements.SeedData
{
    public static class GeoBranchSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("reports");
            // Seed Branches
            modelBuilder.Entity<Branch>().HasData(
                new Branch { BranchId = 1, Name = "HQ", Address = "City Center", IsActive = true },
                new Branch { BranchId = 2, Name = "West Branch", Address = "West Road", IsActive = true }
            );
            // Seed Geolocations
            modelBuilder.Entity<Geolocation>().HasData(
                // Geolocations for HQ
                new Geolocation { GeolocationId = 1, Name = "HQ Main Gate", Latitude = "23.5859", Longitude = "58.4059", RediusMeters = 100, BranchId = 1 },
                new Geolocation { GeolocationId = 2, Name = "HQ Parking", Latitude = "23.5865", Longitude = "58.4065", RediusMeters = 50, BranchId = 1 },
                new Geolocation { GeolocationId = 3, Name = "HQ Backdoor", Latitude = "23.5870", Longitude = "58.4070", RediusMeters = 75, BranchId = 1 },
                // Geolocations for West Branch
                new Geolocation { GeolocationId = 4, Name = "West Main", Latitude = "23.6000", Longitude = "58.4200", RediusMeters = 100, BranchId = 2 },
                new Geolocation { GeolocationId = 5, Name = "West Parking", Latitude = "23.6010", Longitude = "58.4210", RediusMeters = 50, BranchId = 2 },
                new Geolocation { GeolocationId = 6, Name = "West Backdoor", Latitude = "23.6020", Longitude = "58.4220", RediusMeters = 75, BranchId = 2 }
            );
        }
    }

    //Unit tests for CRUD

}
