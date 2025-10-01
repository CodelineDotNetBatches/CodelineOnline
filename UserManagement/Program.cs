using Microsoft.EntityFrameworkCore;
using UserManagement;
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
