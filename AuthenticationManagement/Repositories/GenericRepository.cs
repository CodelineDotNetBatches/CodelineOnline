namespace AuthenticationManagement.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AuthenticationDbContext context;

        public GenericRepository(AuthenticationDbContext context)
        {
            this.context = context;
        }
    }
}