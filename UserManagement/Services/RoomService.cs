// Services/RoomService.cs
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Caching;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;
        private readonly ICacheService _cache;

        private static readonly TimeSpan DefaultTtl = TimeSpan.FromMinutes(5);

        // Cache keys
        private const string AllRoomsKey = "rooms:all";                                  
        private const string AllRoomsWithBranchKey = "rooms:all:includeBranch:true";    
        private static string RoomKey(string roomNumber, bool includeBranch)
            => $"room:{roomNumber}:includeBranch:{includeBranch}".ToLowerInvariant();
        private static string RoomsByBranchKey(int branchId, bool includeBranch)
            => $"rooms:branch:{branchId}:includeBranch:{includeBranch}";

        public RoomService(IRoomRepository repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

       
        public async Task<List<Room>> GetAllAsync(bool includeBranch = false)
        {
            var cacheKey = includeBranch ? AllRoomsWithBranchKey : AllRoomsKey;

            var cached = await _cache.GetAsync<List<Room>>(cacheKey);
            if (cached is not null) return cached;

            IQueryable<Room> q = _repo.Query();

            if (includeBranch)
                q = q.Include(r => r.branchs);

            var result = await q.AsNoTracking().ToListAsync();
            await _cache.SetAsync(cacheKey, result, DefaultTtl);
            return result;
        }

        public async Task<Room?> GetByNumberAsync(string roomNumber, bool includeBranch = false)
        {
            var cacheKey = RoomKey(roomNumber, includeBranch);
            var cached = await _cache.GetAsync<Room>(cacheKey);
            if (cached is not null) return cached;

            var room = await _repo.GetByNumberAsync(roomNumber, includeBranch);
            if (room is not null)
                await _cache.SetAsync(cacheKey, room, DefaultTtl);

            return room;
        }

        public async Task<List<Room>> GetByBranchAsync(int branchId, bool includeBranch = false)
        {
            var cacheKey = RoomsByBranchKey(branchId, includeBranch);
            var cached = await _cache.GetAsync<List<Room>>(cacheKey);
            if (cached is not null) return cached;

            var rooms = await _repo.GetByBranchAsync(branchId, includeBranch);
            await _cache.SetAsync(cacheKey, rooms, DefaultTtl);
            return rooms;
        }

        public async Task<Room> CreateAsync(Room room)
        {
            if (await _repo.ExistsAsync(room.RoomNumber))
                throw new InvalidOperationException($"Room '{room.RoomNumber}' already exists.");

            await _repo.AddAsync(room);
            await _repo.SaveAsync();

            

            return room;
        }

        public async Task<bool> UpdateAsync(string roomNumber, Room updated)
        {
            var existing = await _repo.GetByNumberAsync(roomNumber, includeBranch: false);
            if (existing is null) return false;

            existing.RoomType = updated.RoomType;
            existing.Description = updated.Description;
            existing.Capacity = updated.Capacity;
            existing.BranchId = updated.BranchId;

            _repo.Update(existing);
            await _repo.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(string roomNumber       )
        {
            var existing = await _repo.GetByNumberAsync(roomNumber, includeBranch: false);
            if (existing is null) return false;

            _repo.Delete(existing);
            await _repo.SaveAsync();

            return true;
       

        
        }
    }
}
