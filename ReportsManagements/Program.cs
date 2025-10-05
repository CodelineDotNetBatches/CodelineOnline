using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsManagements.Mapping;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using ReportsManagements.SeedData;
using ReportsManagements.Services;
using Microsoft.EntityFrameworkCore.SqlServer;



//testing branching
//testing 2nd line
//Display grades
namespace ReportsManagements
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //var context = ReasonAndFileSeedData.CreateInMemoryDbContext();
            //builder.Services.AddSingleton(context);
            // ====== Services ======
            builder.Services.AddScoped<IAttendanceRecordService, AttendanceRecordService>();
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddDbContext<ReportsDbContext>(/* SqlServer or InMemory */);
            builder.Services.AddScoped<ITrainerReportRepository, TrainerReportRepository>();
            builder.Services.AddScoped<ICourseReportRepository, CourseReportRepository>();
            builder.Services.AddScoped<IReportsService, ReportsService>();

            builder.Services.AddScoped<ITrainerReportRepository, TrainerReportRepository>();
            builder.Services.AddScoped<ICourseReportRepository, CourseReportRepository>();
            builder.Services.AddScoped<IReportsService, ReportsService>();
            // DbContext
            builder.Services.AddDbContext<ReportsDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default"),
                    sql => sql.MigrationsHistoryTable("__Migrations_App")
                )

            );
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
       
          
            //builder.Services.AddSingleton<IReasonCodeRepository>(new ReasonCodeRepository(context));
            //builder.Services.AddSingleton<IFileStorageRepository>(new FileStorageRepository(context));

            //// ????? Repositories ?? Singleton
            //builder.Services.AddSingleton<IBranchRepository>(new BranchRepository(context));
            //builder.Services.AddSingleton<IGeolocationRepository>(new GeolocationRepository(context));

            // ????? Controllers
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(ReportsManagements.Mapping.ReportsMapping).Assembly);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(ReportsManagements.Mapping.ReportsMapping).Assembly);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(AttendanceMapping));

            var app = builder.Build();

            // Swagger ??? ?? ???????
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            //await PrintSeedData(context);
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ReportsDbContext>();
                DatabaseSeeder.Seed(db);
            }

            app.Run();
        }
        
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