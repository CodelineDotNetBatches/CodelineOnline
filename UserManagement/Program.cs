using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

using UserManagement;                       // UsersDbContext
using UserManagement.Caching;               // ICacheService, MemoryCacheService
using UserManagement.Repositories;          // Repos interfaces & impls
using UserManagement.Services;              // Services interfaces & impls
using UserManagement.Mapping;               // AutoMapper profiles (BatchMapping, etc.)
using UserManagement.Controllers.Middleware;// ErrorHandlingMiddleware
using UserManagement.SeedData;              // BatchSeedData, TraineeSeedData, etc.

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
                    sql => sql.MigrationsHistoryTable("__Migrations_App", "users") // 👈 keep history inside 'users' schema
                )
            );

            // ========================================
            // 2) Caching
            // ========================================
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<ICacheService, MemoryCacheService>();

            // ========================================
            // 3) Repositories
            // ========================================
            builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
            builder.Services.AddScoped<IBatchRepository, BatchRepository>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
            builder.Services.AddScoped<IInstructorSkillRepository, InstructorSkillRepository>();
            builder.Services.AddScoped<IAdminProfileRepository, AdminProfileRepository>();

            // ========================================
            // 4) Services
            // ========================================
            builder.Services.AddScoped<ITraineeService, TraineeService>();
            builder.Services.AddScoped<IBatchService, BatchService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
            builder.Services.AddScoped<IInstructorSkillService, InstructorSkillService>();
            builder.Services.AddScoped<IAdminProfileService, AdminProfileService>();


            // ========================================
            // 5) AutoMapper (scan all mapping profiles)
            // ========================================
            builder.Services.AddAutoMapper(typeof(BatchMapping).Assembly);

            // ========================================
            // 6) Controllers, Swagger, Middleware
            // ========================================
            builder.Services.AddTransient<ErrorHandlingMiddleware>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // ========================================
            // 7) Apply Migrations & Seed Data
            // ========================================
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
                await db.Database.MigrateAsync();

                // ✅ optional: clear tracking cache to ensure seed data consistency
                db.ChangeTracker.Clear();

            
           
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
