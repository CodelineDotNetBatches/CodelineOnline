using UserManagement.Models;

namespace UserManagement.Repositories
{
    /// <summary>
    /// Repository for trainee persistence.
    /// </summary>
    public class TraineeRepository : ITraineeRepository
    {
        private readonly CoursesDbContext _context;

        public TraineeRepository(CoursesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trainee>> GetAllAsync()
        {
            return await _context.Trainees.ToListAsync();
        }

        public async Task<Trainee?> GetByIdAsync(Guid id)
        {
            return await _context.Trainees.FindAsync(id);
        }

        public async Task AddAsync(Trainee trainee)
        {
            await _context.Trainees.AddAsync(trainee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Trainee trainee)
        {
            _context.Trainees.Update(trainee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var trainee = await _context.Trainees.FindAsync(id);
            if (trainee != null)
            {
                _context.Trainees.Remove(trainee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
