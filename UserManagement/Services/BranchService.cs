using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Caching;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class BranchService : IBranchService
    {
        private readonly IGenericRepo<Branch> _repo;
        private readonly ICacheService _cache;
        private readonly IMapper _mapper;

        private const string AllBranchesCacheKey = "all_branches";
        private const string BranchCacheKeyPrefix = "branch_";

        public BranchService(IGenericRepo<Branch> repo, ICacheService cache, IMapper mapper)
        {
            _repo = repo;
            _cache = cache;
            _mapper = mapper;
        }

        // -----------------------
        // Get all branches (cached)
        // -----------------------
        public async Task<IEnumerable<BranchDTO>> GetAllAsync()
        {
            var cached = await _cache.GetAsync<IEnumerable<BranchDTO>>(AllBranchesCacheKey);
            if (cached is not null)
                return cached;

            var branches = _repo.GetAll();
            var result = _mapper.Map<IEnumerable<BranchDTO>>(branches);

            await _cache.SetAsync(AllBranchesCacheKey, result, TimeSpan.FromMinutes(10));
            return result;
        }

        // -----------------------
        // Get single branch (cached)
        // -----------------------
        public async Task<BranchDTO?> GetByIdAsync(int id)
        {
            string key = $"{BranchCacheKeyPrefix}{id}";

            var cached = await _cache.GetAsync<BranchDTO>(key);
            if (cached is not null)
                return cached;

            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)
                return null;

            var dto = _mapper.Map<BranchDTO>(entity);
            await _cache.SetAsync(key, dto, TimeSpan.FromMinutes(10));

            return dto;
        }

        // -----------------------
        // Add new branch
        // -----------------------
        public async Task AddAsync(BranchDTO dto)
        {
            var entity = _mapper.Map<Branch>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();

            // clear cache
            await _cache.RemoveAsync(AllBranchesCacheKey);
        }

        // -----------------------
        // Update branch
        // -----------------------
        public async Task UpdateAsync(int id, BranchDTO dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException($"Branch with ID {id} not found.");

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            await _repo.SaveAsync();


            await _cache.RemoveAsync(AllBranchesCacheKey);
            await _cache.RemoveAsync($"{BranchCacheKeyPrefix}{id}");
        }

        // -----------------------
        // Delete branch
        // -----------------------
        public async Task DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException($"Branch with ID {id} not found.");

            _repo.Delete(existing);
            await _repo.SaveAsync();

            await _cache.RemoveAsync(AllBranchesCacheKey);
            await _cache.RemoveAsync($"{BranchCacheKeyPrefix}{id}");
        }

        // -----------------------
        // filteration branch which has more that 3 batch
        // -----------------------
        public async Task<IEnumerable<BranchDTO>> GetBranchesWithMoreThanThreeBatchesAsync()
        {
            var branches = _repo.GetAll().Where(b => b.batches.Count() > 3);
            var result = _mapper.Map<IEnumerable<BranchDTO>>(branches);
            return await Task.FromResult(result);
        }

    }
}
