using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private static readonly string AllInstructorsCacheKey = "instructors:paged";

        public InstructorService(IInstructorRepository repo, IMapper mapper, IMemoryCache cache)
        {
            _repo = repo; _mapper = mapper; _cache = cache;
        }

        public async Task<InstructorReadDto> CreateFromUserAsync(InstructorCreateDto dto, CancellationToken ct = default)
        {
            // PK is provided (same as UserId); uniqueness guaranteed by key
            var entity = _mapper.Map<Instructor>(dto);
            await _repo.AddAsync(entity, ct);
            await _repo.SaveAsync(ct);

            _cache.Remove(AllInstructorsCacheKey);
            return _mapper.Map<InstructorReadDto>(entity);
        }

        public async Task<InstructorReadDto?> GetAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetAsync(id, ct);
            return entity is null ? null : _mapper.Map<InstructorReadDto>(entity);
        }

        public IEnumerable<InstructorReadDto> GetAll(PagingFilter filter)
        {
            // Try cache (simple, you can key by filter if needed)
            if (_cache.TryGetValue(AllInstructorsCacheKey, out IEnumerable<InstructorReadDto> cached))
                return cached;

            var q = _repo.Query();

            if (!string.IsNullOrEmpty(filter.Expertise))
                q = q.Where(i => i.GithubUserName.Contains(filter.Expertise)); // placeholder for specialization field

            if (filter.Level.HasValue)
                q = q.Where(i => i.Experience_Level == filter.Level.Value);

            if (filter.Style.HasValue)
                q = q.Where(i => i.Teaching_Style == filter.Style.Value);

            q = q.OrderBy(i => i.InstructorId);

            var skip = (filter.Page - 1) * filter.PageSize;

            var result = q
                .Skip(skip).Take(filter.PageSize)
                .AsNoTracking()
                .ProjectTo<InstructorReadDto>(_mapper.ConfigurationProvider)
                .ToList();

            _cache.Set(AllInstructorsCacheKey, result, TimeSpan.FromMinutes(5));
            return result;
        }

        public async Task<IEnumerable<InstructorReadDto>> GetAllAsync(PagingFilter filter, CancellationToken ct = default)
        {
            var q = _repo.Query();

            if (!string.IsNullOrEmpty(filter.Expertise))
                q = q.Where(i => i.GithubUserName.Contains(filter.Expertise));

            if (filter.Level.HasValue)
                q = q.Where(i => i.Experience_Level == filter.Level.Value);

            if (filter.Style.HasValue)
                q = q.Where(i => i.Teaching_Style == filter.Style.Value);

            q = q.OrderBy(i => i.InstructorId);

            var skip = (filter.Page - 1) * filter.PageSize;

            return await q.Skip(skip).Take(filter.PageSize)
                .AsNoTracking()
                .ProjectTo<InstructorReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }

        public async Task<InstructorReadDto> UpdateAsync(int id, InstructorUpdateDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetAsync(id, ct) ?? throw new KeyNotFoundException($"Instructor {id} not found");
            _mapper.Map(dto, entity);
            _repo.Update(entity);
            await _repo.SaveAsync(ct);
            _cache.Remove(AllInstructorsCacheKey);
            return _mapper.Map<InstructorReadDto>(entity);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetAsync(id, ct) ?? throw new KeyNotFoundException($"Instructor {id} not found");
            _repo.Remove(entity);
            await _repo.SaveAsync(ct);
            _cache.Remove(AllInstructorsCacheKey);
        }
    }


}