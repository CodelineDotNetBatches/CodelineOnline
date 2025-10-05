//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ReportsManagements.Mapping;
//using ReportsManagements.Models;
//using ReportsManagements.Repositories;
//using ReportsManagements.SeedData;
//using ReportsManagements.Services;
//using Microsoft.EntityFrameworkCore.SqlServer;



////testing branching
////testing 2nd line
////Display grades
//namespace ReportsManagements
//{
//    public class Program
//    {
//        public static async Task Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            //var context = ReasonAndFileSeedData.CreateInMemoryDbContext();
//            //builder.Services.AddSingleton(context);
//            // ====== Services ======
//            builder.Services.AddScoped<IAttendanceRecordService, AttendanceRecordService>();
//            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
//            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
//            builder.Services.AddScoped<IGeolocationRepository, GeolocationRepository>();
//            builder.Services.AddScoped<IGeoValidationService, GeoValidationService>();

//            // DbContext
//            builder.Services.AddDbContext<ReportsDbContext>(options =>
//                options.UseSqlServer(
//                    builder.Configuration.GetConnectionString("Default"),
//                    sql => sql.MigrationsHistoryTable("__Migrations_App")
//                )
//            );

//            //builder.Services.AddSingleton<IReasonCodeRepository>(new ReasonCodeRepository(context));
//            //builder.Services.AddSingleton<IFileStorageRepository>(new FileStorageRepository(context));

//            //// ????? Repositories ?? Singleton
//            //builder.Services.AddSingleton<IBranchRepository>(new BranchRepository(context));
//            //builder.Services.AddSingleton<IGeolocationRepository>(new GeolocationRepository(context));

//            // ????? Controllers
//            builder.Services.AddControllers();
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();


//            // AutoMapper
//            builder.Services.AddAutoMapper(typeof(AttendanceMapping)); 

//            var app = builder.Build();

//            // Swagger ??? ?? ???????
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();
//            app.UseAuthorization();
//            app.MapControllers();

//            //await PrintSeedData(context);

//            app.Run();
//        }

//        private static async Task PrintSeedData(ReportsDbContext context)
//        {
//            var allReasons = await context.ReasonCodes.ToListAsync();
//            Console.WriteLine("Reason Codes:");
//            foreach (var r in allReasons)
//            {
//                Console.WriteLine($"{r.ReasonCodeId} - {r.Name} ({r.Code}) - {r.Category}");
//            }

//            var allFiles = await context.FileStorages.ToListAsync();
//            Console.WriteLine("\nFile Storage:");
//            foreach (var f in allFiles)
//            {
//                Console.WriteLine($"{f.FileStorageId} - {f.FileName} - {f.Url} - Uploaded by {f.UploadedBy}");
//            }
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsManagements.Mapping;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using ReportsManagements.Services;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ReportsManagements
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ====== Services ======
            builder.Services.AddScoped<IAttendanceRecordService, AttendanceRecordService>();
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IGeolocationRepository, GeolocationRepository>();
            builder.Services.AddScoped<IGeoValidationService, GeoValidationService>();

            // DbContext
            builder.Services.AddDbContext<ReportsDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default"),
                    sql => sql.MigrationsHistoryTable("__Migrations_App")
                )
            );

            // Controllers & Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(AttendanceMapping));

            var app = builder.Build();

            // Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // ✅ Apply migrations + Seed data
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ReportsDbContext>();
                context.Database.Migrate();
                await SeedBranchesAndGeolocations(context);
            }

            app.Run();
        }

        // Seed data for branches and geolocations
        private static async Task SeedBranchesAndGeolocations(ReportsDbContext context)
        {
            if (!context.Branches.Any())
            {
                var branch1 = new Branch { Name = "Main Branch", Address = "123 Street" };
                var branch2 = new Branch { Name = "East Branch", Address = "456 Avenue" };

                context.Branches.AddRange(branch1, branch2);
                await context.SaveChangesAsync();

                var geo1 = new Geolocation
                {
                    Name = "Main Branch Geo",
                    Latitude = "23.585",
                    Longitude = "58.405",
                    RediusMeters = 500, // أو غيّرها إلى RadiusMeters بعد التصحيح
                    BranchId = branch1.BranchId
                };
                var geo2 = new Geolocation
                {
                    Name = "East Branch Geo",
                    Latitude = "23.589",
                    Longitude = "58.410",
                    RediusMeters = 300,
                    BranchId = branch2.BranchId
                };

                context.Geolocations.AddRange(geo1, geo2);
                await context.SaveChangesAsync();
            }
        }
    }
}

