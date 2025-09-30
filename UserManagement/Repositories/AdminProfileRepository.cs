namespace UserManagement.Repositories
{
    public class AdminProfileRepository
    {
        private readonly UsersDbContext _context; // Database context


        public AdminProfileRepository(UsersDbContext context) // Constructor injection of DbContext
        {
            _context = context;
        }



    }
}
