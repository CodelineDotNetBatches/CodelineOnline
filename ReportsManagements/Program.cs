
using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using ReportsManagements.SeedData;



namespace ReportsManagements
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var context = ReasonAndFileSeedData.CreateInMemoryDbContext();
            builder.Services.AddSingleton(context);


            builder.Services.AddSingleton<IReasonCodeRepository>(new ReasonCodeRepository(context));
            builder.Services.AddSingleton<IFileStorageRepository>(new FileStorageRepository(context));

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

            await PrintSeedData(context);

            app.Run();
        }

        private static async Task PrintSeedData(ReportsDbContext context)
        {
            var allReasons = await context.ReasonCodes.ToListAsync();
            Console.WriteLine("Reason Codes:");
            foreach (var r in allReasons)
            {
                Console.WriteLine($"{r.ReasonCodeId} - {r.Name} ({r.Code}) - {r.Category}");
            }

            var allFiles = await context.FileStorages.ToListAsync();
            Console.WriteLine("\nFile Storage:");
            foreach (var f in allFiles)
            {
                Console.WriteLine($"{f.FileStorageId} - {f.FileName} - {f.Url} - Uploaded by {f.UploadedBy}");
            }
        }
    }
}