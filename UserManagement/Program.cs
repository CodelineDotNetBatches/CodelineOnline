using Microsoft.EntityFrameworkCore;
using UserManagement;
using UserManagement.Controllers.Middleware;
using UserManagement.Repositories;
using UserManagement.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection; // for extension method
using UserManagement.mapping;
using UserManagement.Caching;

using UserManagement.Controllers;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.SeedData;
using UserManagement.Repositories;
using UserManagement.Services;
using UserManagement.Mapping;
using UserManagement;

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

            // ========================================
            // 3. Register Repositories
            // ========================================
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
            builder.Services.AddScoped<IBatchRepository, BatchRepository>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();

            // If you add a generic repository:
            // builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            // ========================================
            // 4. Register Services
            // ========================================
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddScoped<ITraineeService, TraineeService>();
            builder.Services.AddScoped<IBatchService, BatchService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();

            // ========================================
            // 5. AutoMapper
            // ========================================
            //builder.Services.AddAutoMapper(typeof(SkillTraineeMappingProfiles));

            builder.Services.AddAutoMapper(
                typeof(AvailabilityMappingProfile).Assembly,
                typeof(InstructorMappingProfile).Assembly,
                typeof(SkillTraineeMappingProfiles).Assembly
            );

            // ========================================
            // 6. Controllers + Swagger 
            // ========================================
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ========================================
            // 7. Error Handling Middleware
            //===================================


            builder.Services.AddTransient<ErrorHandlingMiddleware>();

            // ========================================
            // 8. Build the app
            // ========================================

            var app = builder.Build();


            // Configure the HTTP request pipeline.

            // ========================================
            // 1. Configure Middleware Pipeline
            // ========================================
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
