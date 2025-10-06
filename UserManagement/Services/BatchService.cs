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
                Name = b.BatchName,
                Status = b.BatchStatus,
                StartDate = b.BatchStartDate,
                EndDate = b.BatchEndDate,
                Timeline = b.BatchTimeline,
                Description = b.BatchDescription
            });
        }

        public async Task<BatchDTO?> GetBatchByIdAsync(int id)
        {
            var b = await _repository.GetByIdAsync(id);
            if (b == null) return null;

            return new BatchDTO
            {
                BatchId = b.BatchId,
                Name = b.BatchName,
                Status = b.BatchStatus,
                StartDate = b.BatchStartDate,
                EndDate = b.BatchEndDate,
                Timeline = b.BatchTimeline,
                Description = b.BatchDescription
            };
        }

        public async Task<BatchDTO> CreateBatchAsync(BatchDTO dto)
        {
            var batch = new Batch
            {
                BatchId = dto.BatchId,

                BatchName = dto.Name,
                BatchStatus = dto.Status,
                BatchStartDate = dto.StartDate,
                BatchEndDate = dto.EndDate,
                BatchTimeline = dto.Timeline,
                BatchDescription = dto.Description
            };

            await _repository.AddAsync(batch);
            dto.BatchId = batch.BatchId;
            return dto;
        }

        public async Task<BatchDTO> UpdateBatchAsync(BatchDTO dto)
        {
            var batch = await _repository.GetByIdAsync(dto.BatchId);
            if (batch == null) throw new Exception("Batch not found");

            batch.BatchName = dto.Name;
            batch.BatchStatus = dto.Status;
            batch.BatchStartDate = dto.StartDate;
            batch.BatchEndDate = dto.EndDate;
            batch.BatchTimeline = dto.Timeline;
            batch.BatchDescription = dto.Description;

            await _repository.UpdateAsync(batch);

            return dto;
        }

        public async Task DeleteBatchAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
