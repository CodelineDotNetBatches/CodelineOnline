using Microsoft.EntityFrameworkCore;
using ReportsManagements;
using ReportsManagements.SeedData;
using ReportsManagements.Repositories;
using ReportsManagements.Services;
using ReportsManagements.Mapping;
using AutoMapper;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddDbContext<ReportsDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
        sql => sql.MigrationsHistoryTable("__Migrations_App")));
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IBranchRepository, BranchRepository>();
        builder.Services.AddScoped<BranchService>();
        builder.Services.AddAutoMapper(typeof(Branch_mapping));
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
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ReportsDbContext>();
            DatabaseSeeder.Seed(context);
        }
        app.Run();
    }
}