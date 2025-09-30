
using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using ReportsManagements.Services;

namespace ReportsManagements
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add services to the container(Dbcontext).
            builder.Services.AddDbContext<ReportsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
                sql => sql.MigrationsHistoryTable("__Migrations_App")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            // builder.Services.AddScoped<IAttendanceService, AttendanceService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            //
            using var scope = app.Services.CreateScope(); var context = scope.ServiceProvider.GetRequiredService<ReportsDbContext>(); if (!context.Geolocations.Any()) { context.Geolocations.Add(new Geolocation { Latitude = "23.6", Longitude = "58.5" }); context.SaveChanges(); }
            if (!context.FileStorages.Any())
            {
                context.FileStorages.Add(new ReportsManagements.Models.FileStorage
                {
                    FileName = "test.jpg",
                    Url = "/uploads/test.jpg",
                    UploadedAt = DateTime.UtcNow,
                    UploadedBy = "System"
                });

                // Minimal API CRUD Endpoints
                app.MapPost("/api/v1/attendance", async (AttendanceRecord record, IAttendanceRecordService service) =>
            {
                var created = await service.CreateAsync(record);
                return Results.Created($"/api/v1/attendance/{created.AttId}", created);
            });

                app.MapGet("/api/v1/attendance/{id}", async (int id, IAttendanceRecordService service) =>
                {
                    var record = await service.GetByIdAsync(id);
                    return record != null ? Results.Ok(record) : Results.NotFound();
                });

                app.MapPut("/api/v1/attendance/{id}", async (int id, AttendanceRecord updated, IAttendanceRecordService service) =>
                {
                    var record = await service.UpdateAsync(id, updated);
                    return record != null ? Results.Ok(record) : Results.NotFound();
                });

                app.MapDelete("/api/v1/attendance/{id}", async (int id, IAttendanceRecordService service) =>
                {
                    var deleted = await service.DeleteAsync(id);
                    return deleted ? Results.NoContent() : Results.NotFound();
                });


                //run app


                app.Run();
            }
        }
    }
}
