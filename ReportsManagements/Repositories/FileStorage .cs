namespace ReportsManagements.Repositories
{
    public class FileStorage
    {
        private readonly ReportsDbContext _context;

        public FileStorage(ReportsDbContext context)
        {
            _context = context;
        }
    }
}
