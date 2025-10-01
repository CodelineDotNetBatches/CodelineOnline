using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    /// <summary>
    /// Service implementation for batch operations.
    /// </summary>
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _repository;

        public BatchService(IBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BatchDTO>> GetAllBatchesAsync()
        {
            var batches = await _repository.GetAllAsync();
            return batches.Select(b => new BatchDTO
            {
                BatchId = b.BatchId,
                Name = b.Name,
                Status = b.Status,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Timeline = b.Timeline,
                Description = b.Description
            });
        }

        public async Task<BatchDTO?> GetBatchByIdAsync(Guid id)
        {
            var b = await _repository.GetByIdAsync(id);
            if (b == null) return null;

            return new BatchDTO
            {
                BatchId = b.BatchId,
                Name = b.Name,
                Status = b.Status,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Timeline = b.Timeline,
                Description = b.Description
            };
        }

        public async Task<BatchDTO> CreateBatchAsync(BatchDTO dto)
        {
            var batch = new Batch
            {
                BatchId = Guid.NewGuid(),
                Name = dto.Name,
                Status = dto.Status,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Timeline = dto.Timeline,
                Description = dto.Description
            };

            await _repository.AddAsync(batch);
            dto.BatchId = batch.BatchId;
            return dto;
        }

        public async Task<BatchDTO> UpdateBatchAsync(BatchDTO dto)
        {
            var batch = await _repository.GetByIdAsync(dto.BatchId);
            if (batch == null) throw new Exception("Batch not found");

            batch.Name = dto.Name;
            batch.Status = dto.Status;
            batch.StartDate = dto.StartDate;
            batch.EndDate = dto.EndDate;
            batch.Timeline = dto.Timeline;
            batch.Description = dto.Description;

            await _repository.UpdateAsync(batch);

            return dto;
        }

        public async Task DeleteBatchAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
