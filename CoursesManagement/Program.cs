using CoursesManagement;
using CoursesManagement.Mapping;
using CoursesManagement.Repos;
using CoursesManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. Database Context (EF Core + Lazy Loading)
// ==========================================
builder.Services.AddDbContext<CoursesDbContext>(options =>
    options.UseLazyLoadingProxies()
           .UseSqlServer(
               builder.Configuration.GetConnectionString("Default"),
               sql => sql.MigrationsHistoryTable("__Migrations_App", "Courses")
           ));

// ==========================================
// 2. Repositories (DAL)
// ==========================================
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IProgramsRepo, ProgramsRepo>();
builder.Services.AddScoped<ICourseRepo, CourseRepo>();
builder.Services.AddScoped<IEnrollmentRepo, EnrollmentRepo>();

// ==========================================
// 3. Services (BLL)
// ==========================================
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProgramsService, ProgramsService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

// ==========================================
// 4. AutoMapper (Profiles)
// ==========================================
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ==========================================
// 5. Caching
// ==========================================
builder.Services.AddMemoryCache();

// ==========================================
// 6. Controllers + JSON Options
// ==========================================
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

// ==========================================
// 7. Swagger Documentation
// ==========================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Courses Management API",
        Description = "API for managing Programs, Categories, and Courses",
        Contact = new OpenApiContact
        {
            Name = "CodeLine Online",
            Url = new Uri("https://github.com/CodelineDotNetBatches/CodelineOnline")
        }
    });
    options.EnableAnnotations(); // For [SwaggerResponse], [SwaggerOperation]
});

// ==========================================
// 8. Build App
// ==========================================
var app = builder.Build();

// ==========================================
// 9. Middleware Pipeline
// ==========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
