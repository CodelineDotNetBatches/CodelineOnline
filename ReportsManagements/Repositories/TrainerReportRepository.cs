using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public class TrainerReportRepository : ITrainerReportRepository
    {
        private readonly ReportsDbContext _context;

        public TrainerReportRepository(ReportsDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TrainerReport>> GetAllAsync() =>
          await _context.TrainerReports.ToListAsync();


        public async Task<TrainerReport?> GetByIdAsync(int id)
        {
            return await _context.TrainerReports.FindAsync(id);
        }
        public async Task<TrainerReport> AddAsync(TrainerReport trainerReport)
        {
            _context.TrainerReports.Add(trainerReport);
            await _context.SaveChangesAsync();
            return trainerReport;
        }
        public async Task<TrainerReport?> UpdateAsync(TrainerReport trainerReport)
        {
            _context.TrainerReports.Update(trainerReport);
            await _context.SaveChangesAsync();
            return trainerReport;
        }

        public async Task DeleteAsync(int id)
        {
            var trainerReport = await _context.TrainerReports.FindAsync(id);
            if (trainerReport != null)
            {
                _context.TrainerReports.Remove(trainerReport);
                await _context.SaveChangesAsync();
            }
        }


    }
}
