using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public AvailabilityService(IAvailabilityRepository repo, IMapper mapper, IMemoryCache cache)
        { _repo = repo; _mapper = mapper; _cache = cache; }

        public async Task<AvailabilityReadDto> AddAsync(AvailabilityCreateDto dto, CancellationToken ct = default)
        {
            // double-booking guard (relies on unique index too)
            var exists = await _repo.Query().AnyAsync(a =>
                a.InstructorId == dto.InstructorId &&
                a.day_of_week == dto.Day_Of_Week &&
                a.time == dto.Time, ct);

            if (exists) throw new InvalidOperationException("Slot already exists for this instructor.");

            var nextId = await _repo.NextAvailabilityIdAsync(dto.InstructorId, ct);
            var entity = _mapper.Map<Availability>(dto);
            entity.avilabilityId = nextId;

            await _repo.AddAsync(entity, ct);
            await _repo.SaveAsync(ct);

            _cache.Remove(CalendarKey(dto.InstructorId));
            return _mapper.Map<AvailabilityReadDto>(entity);
        }

        public async Task<AvailabilityReadDto> UpdateAsync(int instructorId, int availabilityId, AvailabilityUpdateDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetAsync(instructorId, availabilityId, ct) ?? throw new KeyNotFoundException("Availability not found.");

            // if day/time changed, enforce double-booking rule
            bool slotChange = entity.day_of_week != dto.Day_Of_Week || entity.time != dto.Time;
            if (slotChange)
            {
                var conflict = await _repo.Query().AnyAsync(a =>
                    a.InstructorId == instructorId &&
                    a.day_of_week == dto.Day_Of_Week &&
                    a.time == dto.Time &&
                    a.avilabilityId != availabilityId, ct);
                if (conflict) throw new InvalidOperationException("New slot conflicts with existing one.");
            }

            _mapper.Map(dto, entity);
            _repo.Update(entity);
            await _repo.SaveAsync(ct);

            _cache.Remove(CalendarKey(instructorId));
            return _mapper.Map<AvailabilityReadDto>(entity);
        }

        public async Task RemoveAsync(int instructorId, int availabilityId, CancellationToken ct = default)
        {
            var entity = await _repo.GetAsync(instructorId, availabilityId, ct) ?? throw new KeyNotFoundException("Availability not found.");
            _repo.Remove(entity);
            await _repo.SaveAsync(ct);
            _cache.Remove(CalendarKey(instructorId));
        }

        public async Task<IEnumerable<AvailabilityReadDto>> GetByInstructorAsync(int instructorId, CancellationToken ct = default)
        {
            return await _repo.Query()
                .Where(a => a.InstructorId == instructorId)
                .AsNoTracking()
                .ProjectTo<AvailabilityReadDto>((_mapper.ConfigurationProvider))
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<AvailabilityReadDto>> GenerateCalendarAsync(int instructorId, CancellationToken ct = default)
        {
            // cache per instructor
            if (_cache.TryGetValue(CalendarKey(instructorId), out IEnumerable<AvailabilityReadDto> cached))
                return cached;

            var list = await _repo.Query()
                .Where(a => a.InstructorId == instructorId && a.Avail_Status != AvailabilityStatus.Completed)
                .OrderBy(a => a.day_of_week).ThenBy(a => a.time)
                .AsNoTracking()
                .ProjectTo<AvailabilityReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            _cache.Set(CalendarKey(instructorId), list, TimeSpan.FromMinutes(5));
            return list;
        }

        private static string CalendarKey(int instructorId) => $"calendar:{instructorId}";
    }



}