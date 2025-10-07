using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsManagements;
using ReportsManagements.Mapping;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using ReportsManagements.SeedData;
using ReportsManagements.Services;
using AutoMapper;

namespace ReportsManagements
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ====== Database Context ======
            builder.Services.AddDbContext<ReportsDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default"),
                    sql => sql.MigrationsHistoryTable("__Migrations_App")
                )
            );

            // ====== Repositories ======
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IBranchReportRepository, BranchReportRepository>();
            builder.Services.AddScoped<IGeolocationRepository, GeolocationRepository>();
            builder.Services.AddScoped<IFileStorageRepository, FileStorageRepository>();
            builder.Services.AddScoped<IReasonCodeRepository, ReasonCodeRepository>();
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<ICourseReportRepository, CourseReportRepository>();
            builder.Services.AddScoped<ITrainerReportRepository, TrainerReportRepository>();

            // ====== Services ======
            builder.Services.AddScoped<BranchService>();
            builder.Services.AddScoped<BranchReportService>();
            builder.Services.AddScoped<IGeoValidationService, GeoValidationService>();
            builder.Services.AddScoped<IFileCodeService, FileCodeService>();
            builder.Services.AddScoped<IAttendanceRecordService, AttendanceRecordService>();
            builder.Services.AddScoped<IReportsService, ReportsService>();
            builder.Services.AddScoped<UploadRateLimiterService>();
            builder.Services.AddScoped<CourseReportService>();
            builder.Services.AddScoped<TrainerReportService>();

            builder.Services.AddMemoryCache();

            // ====== AutoMapper Configurations ======
            builder.Services.AddAutoMapper(typeof(Branch_mapping));
            builder.Services.AddAutoMapper(typeof(AttendanceMapping));
            builder.Services.AddAutoMapper(typeof(ReportsMapping));
            builder.Services.AddAutoMapper(typeof(CourseReportMapping));
            builder.Services.AddAutoMapper(typeof(TrainerReportMapping));

            // ====== Controllers & API Behavior ======
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            .Where(x => x.Value.Errors.Count > 0)
                            .ToDictionary(
                                kvp => kvp.Key,
                                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                            );

                        var problemDetails = new ValidationProblemDetails(errors)
                        {
                            Type = "https://httpstatuses.com/400",
                            Title = "Validation Failed",
                            Status = StatusCodes.Status400BadRequest,
                            Instance = context.HttpContext.Request.Path
                        };

                        return new BadRequestObjectResult(problemDetails);
                    };
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ====== Swagger ======
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ====== Middleware ======
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // ====== Database Seeding ======
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ReportsDbContext>();
                DatabaseSeeder.Seed(db);
            }

            app.Run();
        }

        // ====== Optional helper to display seed data in console ======
        private static async Task PrintSeedData(ReportsDbContext context)
        {
            var allReasons = await context.ReasonCodes.ToListAsync();
            Console.WriteLine("Reason Codes:");
            foreach (var r in allReasons)
            {
                Console.WriteLine($"{r.ReasonCodeId} - {r.Name} ({r.Code}) - {r.Category}");
            }

            var allFiles = await context.FileStorages.ToListAsync();
            Console.WriteLine("\nFile Storage:");
            foreach (var f in allFiles)
            {
                Console.WriteLine($"{f.FileStorageId} - {f.FileName} - {f.Url} - Uploaded by {f.UploadedBy}");
            }
        }
    }
}
