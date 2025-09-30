using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement
{
    public class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options) 
        : base(options) 
        { }

       public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "courses"
            mb.HasDefaultSchema("courses");


        }

    }
}
