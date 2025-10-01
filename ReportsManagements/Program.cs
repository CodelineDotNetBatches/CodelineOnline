using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsManagements.Mapping;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using ReportsManagements.SeedData;
using ReportsManagements.Services;

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

            // ====== Middleware ======
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // ====== Seed Data ======
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ReportsDbContext>();
                DatabaseSeeder.Seed(context);
            }

            // Run App
            app.Run();
        }
    }
}
