//using ReportsManagements.Models;

//namespace ReportsManagements.SeedData
//{
//    public static class DatabaseSeeder
//    {
//        public static void Seed(ReportsDbContext context)
//        {
//            context.Database.EnsureCreated();

//            if (!context.Branches.Any())
//            {
//                var hqBranch = new Branch { Name = "HQ", Address = "City Center", IsActive = true };
//                var westBranch = new Branch { Name = "West Branch", Address = "West Road", IsActive = true };

//                context.Branches.AddRange(hqBranch, westBranch);
//                context.SaveChanges();

//                if (!context.Geolocations.Any())
//                {
//                    context.Geolocations.AddRange(
//                        new Geolocation { Name = "HQ Main Gate", Latitude = "23.5859", Longitude = "58.4059", RediusMeters = 100, Branch = hqBranch },
//                        new Geolocation { Name = "HQ Parking", Latitude = "23.5865", Longitude = "58.4065", RediusMeters = 50, Branch = hqBranch },
//                        new Geolocation { Name = "HQ Backdoor", Latitude = "23.5870", Longitude = "58.4070", RediusMeters = 75, Branch = hqBranch },
//                        new Geolocation { Name = "West Main", Latitude = "23.6000", Longitude = "58.4200", RediusMeters = 100, Branch = westBranch },
//                        new Geolocation { Name = "West Parking", Latitude = "23.6010", Longitude = "58.4210", RediusMeters = 50, Branch = westBranch },
//                        new Geolocation { Name = "West Backdoor", Latitude = "23.6020", Longitude = "58.4220", RediusMeters = 75, Branch = westBranch }
//                    );
//                    context.SaveChanges();
//                }
//            }
//        }
//    }
//}
