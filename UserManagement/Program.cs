using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement;                       // UsersDbContext
using UserManagement.Caching;               // ICacheService, MemoryCacheService
using UserManagement.Controllers.Middleware;// ErrorHandlingMiddleware
using UserManagement.Mapping;               // AutoMapper profiles (BatchMapping, etc.)
using UserManagement.Repositories;          // Repository interfaces & implementations
using UserManagement.SeedData;              // Seed data classes
using UserManagement.Services;
// Service interfaces & implementations

namespace CodeLine_Online
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ========================================
            // 1) Database Context
            // ========================================
            builder.Services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default"),
                    sql => sql.MigrationsHistoryTable("__Migrations_App", "users")
                )
            );

            // ========================================
            // 2) Caching
            // ========================================
            builder.Services.AddMemoryCache();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddScoped<ICacheService, MemoryCacheService>();

            // ========================================
            // 3) Repositories
            // ========================================
            builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
            builder.Services.AddScoped<IBatchRepository, BatchRepository>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
            builder.Services.AddScoped<IInstructorSkillRepository, InstructorSkillRepository>();
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepository<>)); // Generic repository
            // Register Admin repository (important for DI)
            builder.Services.AddScoped<IAdminProfileRepository, AdminProfileRepository>();

            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            //builder.Services.AddScoped<IRoomRepository, RoomRepository>();

            // ========================================
            // 4) Services
            // ========================================
            builder.Services.AddScoped<ITraineeService, TraineeService>();
            builder.Services.AddScoped<IBatchService, BatchService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
            builder.Services.AddScoped<IInstructorSkillService, InstructorSkillService>();

            // ✅ Register Admin service (this fixes your Swagger error)
            builder.Services.AddScoped<IAdminProfileService, AdminProfileService>();
            builder.Services.AddScoped<IBranchService, BranchService>();
            //builder.Services.AddScoped<IRoomService, RoomService>();

            // ========================================
            // 5) AutoMapper
            // ========================================
            // Scans all Mapping Profiles in your assembly (Batch, Admin, Instructor, etc.)
            builder.Services.AddAutoMapper(typeof(BatchMapping).Assembly);

            // ========================================
            // 6) Controllers, Swagger, Middleware
            // ========================================
            builder.Services.AddTransient<ErrorHandlingMiddleware>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });


            var app = builder.Build();

            // ========================================
            // 7) Apply Migrations & Seed Data
            // ========================================
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
                await db.Database.MigrateAsync();
                db.ChangeTracker.Clear();

                // Optionally, run seeders:
                // await BatchSeedData.SeedAsync(db);
                // await TraineeSeedData.SeedAsync(db);
                // ✅ You can also seed AdminProfiles here if you wish:
                // await AdminProfileSeedData.SeedAsync(db);
            }

            // ========================================
            // 8) Middleware Pipeline
            // ========================================
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
