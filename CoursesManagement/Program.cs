
using Microsoft.EntityFrameworkCore;
using System;

namespace CoursesManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CoursesDbContext>(options =>
                options.UseLazyLoadingProxies() //to enable Lazy Loading ...
                       .UseSqlServer(
                           builder.Configuration.GetConnectionString("Default"),
                           sql => sql.MigrationsHistoryTable("__Migrations_App", "Courses")
                       ));


            // Add caching ...
            builder.Services.AddMemoryCache();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.Run();
        }
    }
}
