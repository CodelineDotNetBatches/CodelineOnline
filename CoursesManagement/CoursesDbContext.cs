using Microsoft.EntityFrameworkCore;

namespace CoursesManagement
{
    public class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options) 
        : base(options) 
        { }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "courses"
            mb.HasDefaultSchema("courses");


        }

    }
}
