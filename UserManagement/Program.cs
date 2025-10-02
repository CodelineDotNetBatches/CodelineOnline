
using Microsoft.EntityFrameworkCore;
using UserManagement;
using UserManagement.Controllers.Middleware;
using UserManagement.Repositories;
using UserManagement.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection; // for extension method
using UserManagement.mapping;

namespace CodeLine_Online
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
            sql => sql.MigrationsHistoryTable("__Migrations_App")));

            builder.Services.AddAutoMapper(
                typeof(AvailabilityMappingProfile).Assembly,
                typeof(InstructorMappingProfile).Assembly
            );

            //builder.Services.AddAutoMapper(typeof(Program));



            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMemoryCache();
            builder.Services.AddTransient<ErrorHandlingMiddleware>();

            var app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();


            // Configure the HTTP request pipeline.
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
