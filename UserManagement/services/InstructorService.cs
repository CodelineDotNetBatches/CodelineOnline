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

        public async Task<InstructorReadDto> CreateFromUserAsync(InstructorCreateDto dto)
        {
            // PK is provided (same as UserId); uniqueness guaranteed by key
            var entity = _mapper.Map<Instructor>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();

            _cache.Remove(AllInstructorsCacheKey);
            return _mapper.Map<InstructorReadDto>(entity);
        }

        public async Task<InstructorReadDto?> GetAsync(int id)
        {
            var entity = await _repo.GetAsync(id);
            return entity is null ? null : _mapper.Map<InstructorReadDto>(entity);
        }

        public IEnumerable<InstructorReadDto> GetAll(PagingFilter filter)
        {
            // Attempting to read data from cache
            if (_cache.TryGetValue(AllInstructorsCacheKey, out IEnumerable<InstructorReadDto> cached))
                return cached;
            // Create a query from the Repository
            var q = _repo.Query();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.Expertise))
                q = q.Where(i => i.GithubUserName.Contains(filter.Expertise)); // placeholder for specialization field

            if (filter.Level.HasValue)
                q = q.Where(i => i.Experience_Level == filter.Level.Value);

            if (filter.Style.HasValue)
                q = q.Where(i => i.Teaching_Style == filter.Style.Value);

            //Ranking of results

            q = q.OrderBy(i => i.InstructorId);


            // Pagination counting
            var skip = (filter.Page - 1) * filter.PageSize;

            // Execute the query and actually fetch the results
            var result = q
                .Skip(skip).Take(filter.PageSize) // Apply pagination
                .AsNoTracking() // Tells EF Core that it doesn't need to track these objects (speeds up performance because they are read-only)
                .ProjectTo<InstructorReadDto>(_mapper.ConfigurationProvider) // Uses AutoMapper to convert from Instructor to InstructorReadDto
                .ToList(); // Execute the query and get the results as a List

            // Store the result in cache for future requests
            _cache.Set(AllInstructorsCacheKey, result, TimeSpan.FromMinutes(5));

            // return the result
            return result;
        }

        public async Task<IEnumerable<InstructorReadDto>> GetAllAsync(PagingFilter filter)
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
                .ToListAsync();
        }

        public async Task<InstructorReadDto> UpdateAsync(int id, InstructorUpdateDto dto)
        {
            var entity = await _repo.GetAsync(id) ?? throw new KeyNotFoundException($"Instructor {id} not found");
            _mapper.Map(dto, entity);
            _repo.Update(entity);
            await _repo.SaveAsync();
            _cache.Remove(AllInstructorsCacheKey); // invalidate cache
            return _mapper.Map<InstructorReadDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetAsync(id) ?? throw new KeyNotFoundException($"Instructor {id} not found");
            _repo.Remove(entity);
            await _repo.SaveAsync();
            _cache.Remove(AllInstructorsCacheKey); // invalidate cache
        }
    }


}