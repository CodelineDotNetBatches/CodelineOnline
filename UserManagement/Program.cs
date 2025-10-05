using Microsoft.EntityFrameworkCore;
using UserManagement;
using UserManagement.Repositories;
using UserManagement.Services;
using UserManagement.Mapping;
using UserManagement.Controllers.Middleware;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace CodeLine_Online
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ========================================
            // 1. Database Context
            // ========================================

            // Add services to the container.
            //options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
            //sql => sql.MigrationsHistoryTable("__Migrations_App")));




            // ========================================
            // 1. Database Context
            // ========================================
            builder.Services.AddDbContext<UsersDbContext>(options =>

            options.UseSqlServer(
                            builder.Configuration.GetConnectionString("Default"),
                            sql => sql.MigrationsHistoryTable("__Migrations_App", "user_management")

            ));

            // ========================================
            // 2. Caching: In-Memory
            // ==========================================

            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<ICacheService, MemoryCacheService>();
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default"),
                    sql => sql.MigrationsHistoryTable("__Migrations_App", "user_management")
                )
            );

            // ========================================
            // 3. Register Repositories
            // ========================================
            builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
            builder.Services.AddScoped<IBatchRepository, BatchRepository>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
            builder.Services.AddScoped<IBatchRepository, BatchRepository>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();

            // ========================================
            // 4. Register Services
            // ========================================
            builder.Services.AddScoped<ITraineeService, TraineeService>();
            builder.Services.AddScoped<IBatchService, BatchService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddScoped<ITraineeService, TraineeService>();
            builder.Services.AddScoped<IBatchService, BatchService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();

            // ========================================
            // 5. AutoMapper
            // 4. AutoMapper Profiles
            // ========================================
            //builder.Services.AddAutoMapper(typeof(SkillTraineeMappingProfiles));

            builder.Services.AddAutoMapper(
                typeof(AvailabilityMappingProfile).Assembly,
                typeof(InstructorMappingProfile).Assembly,
                typeof(SkillTraineeMappingProfiles).Assembly
            );

            // ========================================
            // 6. Controllers + Swagger 
            // 5. Middleware, Controllers, Swagger
            // ========================================
            builder.Services.AddMemoryCache();
            builder.Services.AddTransient<ErrorHandlingMiddleware>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ========================================
            // 1. Configure Middleware Pipeline
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

            app.Run();
        }
    }
}
