
using Microsoft.EntityFrameworkCore;

namespace ReportsManagements
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //testing

            // Add services to the container.

            // Add services to the container.
            builder.Services.AddDbContext<ReportsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
                sql => sql.MigrationsHistoryTable("__Migrations_App")));

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
