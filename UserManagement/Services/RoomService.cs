// Services/RoomService.cs
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Caching;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;
        private readonly ICacheService _cache;
        private readonly AutoMapper.IMapper _mapper;
        private static readonly TimeSpan DefaultTtl = TimeSpan.FromMinutes(5);

        // Cache keys
        private const string AllRoomsKey = "rooms:all";
        private const string AllRoomsWithBranchKey = "rooms:all:includeBranch:true";
        private static string RoomKey(string roomNumber, bool includeBranch)
            => $"room:{roomNumber}:includeBranch:{includeBranch}".ToLowerInvariant();
        private static string RoomsByBranchKey(int branchId, bool includeBranch)
            => $"rooms:branch:{branchId}:includeBranch:{includeBranch}";

        public RoomService(IRoomRepository repo, ICacheService cache, AutoMapper.IMapper mapper)
        {
            _repo = repo;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<List<RoomDTO>> GetAllAsync(bool includeBranch = false)
        {
            var cacheKey = includeBranch ? AllRoomsWithBranchKey : AllRoomsKey;

            var cached = await _cache.GetAsync<List<RoomDTO>>(cacheKey);
            if (cached is not null)
                return cached;

            IQueryable<Room> q = _repo.Query();

            if (includeBranch)
                q = q.Include(r => r.branchs);

            var result = await q.AsNoTracking().ToListAsync();
            var mapped = _mapper.Map<List<RoomDTO>>(result);

            await _cache.SetAsync(cacheKey, mapped, DefaultTtl);
            return mapped;
        }


        public async Task<RoomDTO?> GetByNumberAsync(string roomNumber, bool includeBranch = false)
        {
            var cacheKey = RoomKey(roomNumber, includeBranch);
            var cached = await _cache.GetAsync<RoomDTO>(cacheKey);
            if (cached is not null)
                return cached;

            var room = await _repo.GetByNumberAsync(roomNumber, includeBranch);
            if (room is null)
                return null;

            var dto = _mapper.Map<RoomDTO>(room);
            await _cache.SetAsync(cacheKey, dto, DefaultTtl);
            return dto;
        }

        public async Task<List<RoomDTO>> GetByBranchAsync(int branchId, bool includeBranch = false)
        {
            var cacheKey = RoomsByBranchKey(branchId, includeBranch);
            var cached = await _cache.GetAsync<List<RoomDTO>>(cacheKey);
            if (cached is not null)
                return cached;

            var rooms = await _repo.GetByBranchAsync(branchId, includeBranch);
            var mapped = _mapper.Map<List<RoomDTO>>(rooms);

            await _cache.SetAsync(cacheKey, mapped, DefaultTtl);
            return mapped;
        }
        public async Task<RoomDTO> CreateAsync(RoomDTO dto)
        {
            if (await _repo.ExistsAsync(dto.RoomNumber))
                throw new InvalidOperationException($"Room '{dto.RoomNumber}' already exists.");

            var entity = _mapper.Map<Room>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();

            // Clear cache after creating
            await _cache.RemoveAsync(AllRoomsKey);
            await _cache.RemoveAsync(AllRoomsWithBranchKey);

            return _mapper.Map<RoomDTO>(entity);
        }

        public async Task<bool> UpdateAsync(string roomNumber, RoomDTO dto)
        {
            var existing = await _repo.GetByNumberAsync(roomNumber, includeBranch: false);
            if (existing is null)
                return false;

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            await _repo.SaveAsync();

            // Clear or refresh cache for this room
            await _cache.RemoveAsync(RoomKey(roomNumber, includeBranch: false));

            return true;
        }

        public async Task<bool> DeleteAsync(string roomNumber)
        {
            var existing = await _repo.GetByNumberAsync(roomNumber, includeBranch: false);
            if (existing is null)
                return false;

            _repo.Delete(existing);
            await _repo.SaveAsync();

            // Remove from cache
            await _cache.RemoveAsync(RoomKey(roomNumber, includeBranch: false));
            await _cache.RemoveAsync(AllRoomsKey);
            await _cache.RemoveAsync(AllRoomsWithBranchKey);

            return true;
        }
    }
}
