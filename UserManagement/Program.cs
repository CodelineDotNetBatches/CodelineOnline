using Microsoft.EntityFrameworkCore;
using UserManagement;
using UserManagement.Repositories;
using UserManagement.Services;
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



            // Register repositories
            builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
            builder.Services.AddScoped<IBatchRepository, BatchRepository>();

            // Register services
            builder.Services.AddScoped<ITraineeService, TraineeService>();
            builder.Services.AddScoped<IBatchService, BatchService>();





            // Add services to the container.
            builder.Services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default"),
                    sql => sql.MigrationsHistoryTable("__Migrations_App", "user_management")
                )
            );

            // ========================================
            // 2. Register Repositories
            // ========================================
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();

            // If you add a generic repository:
            // builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            // ========================================
            // 3. Register Services
            // ========================================
            builder.Services.AddScoped<ISkillService, SkillService>();

            // ========================================
            // 4. AutoMapper
            // ========================================
            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            // ========================================
            // 5. Controllers + Swagger
            // ========================================
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ========================================
            // 6. Configure Middleware Pipeline
            // ========================================
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
