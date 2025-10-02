
using Microsoft.EntityFrameworkCore;
using ReportsManagements.Repositories;
using ReportsManagements.SeedData;

namespace ReportsManagements
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);

            //// Add services to the container.

            //// Add services to the container.
            //builder.Services.AddDbContext<ReportsDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
            //    sql => sql.MigrationsHistoryTable("__Migrations_App")));

            //builder.Services.AddControllers();
            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();

            //app.UseAuthorization();


            //app.MapControllers();



            //app.Run();
            var builder = WebApplication.CreateBuilder(args);

            // ????? DbContext ?? Seed Data
            var context = GeoBranchSeedData.CreateInMemoryDbContext();

            // ????? DbContext ?? Singleton
            builder.Services.AddSingleton(context);

            // ????? Repositories ?? Singleton
            builder.Services.AddSingleton<IBranchRepository>(new BranchRepository(context));
            builder.Services.AddSingleton<IGeolocationRepository>(new GeolocationRepository(context));

            // ????? Controllers
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Swagger ??? ?? ???????
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            // ????? ???????? ??????? (???????)
            await PrintSeedData(context);

            app.Run();

            // --- ???? ???? ???????? ??? ??????? ---
            static async Task PrintSeedData(ReportsDbContext context)
            {
                Console.WriteLine("Branches:");
                var branches = await context.Branches.ToListAsync();
                foreach (var b in branches)
                    Console.WriteLine($"{b.BranchId} - {b.Name} - Active: {b.IsActive}");

                Console.WriteLine("\nGeolocations:");
                var geos = await context.Geolocations.ToListAsync();
                foreach (var g in geos)
                    Console.WriteLine($"{g.GeolocationId} - {g.Name} ({g.Latitude}, {g.Longitude}) - BranchId: {g.BranchId}");
            }
        }
    }
}