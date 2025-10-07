using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly CoursesDbContext _db;
        private readonly IMapper _mapper;
        private readonly ICertificateRepo _repo;

        public CertificateService(CoursesDbContext db, IMapper mapper, ICertificateRepo repo)
        {
            _db = db;
            _mapper = mapper;
            _repo = repo;
        }


        /// Issues a certificate. Enforces: one certificate per Enrollment.
        public async Task<CertificateDetailsDto> IssueAsync(CertificateIssueDto dto)
        {

            var userId = dto.UserId;
            var courseId = dto.CourseId;


            //var enrollment = await _db.Set<Enrollment>()
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);

            // Fix: Ensure `userId` and `courseId` are of the same type as `e.UserId` and `e.CourseId`.
            var enrollment = await _db.Set<Enrollment>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserId == Guid.Parse(userId.ToString()) && e.CourseId == Guid.Parse(courseId.ToString()));

               

            if (enrollment is null)
            {
                throw new InvalidOperationException("Enrollment not found for the specified user and course.");
            }

            // Ensure one certificate per enrollment
            var exists = await _db.Set<Certificate>()
                .AsNoTracking()
                .AnyAsync(c => c.EnrollmentId == enrollment.EnrollmentId);

            if (exists)
            {
                throw new InvalidOperationException("A certificate already exists for this enrollment.");
            }

            // Map & persist
            //var entity = new Certificate
            //{
            //    CertificateId = Guid.NewGuid(),
            //    EnrollmentId   = enrollment.EnrollmentId,
            //    CourseId       = courseId,
            //    UserId         = userId,
            //    CertificateUrl = dto.CertificateUrl,
            //    IssuedAt       = DateTime.UtcNow
            //};
            var entity = _mapper.Map<Certificate>(dto);


            await _db.Set<Certificate>().AddAsync(entity);
            await _db.SaveChangesAsync();

            return _mapper.Map<CertificateDetailsDto>(entity);
        }

        public async Task UpdateUrlAsync(Guid certificateId, CertificateUpdateUrlDto dto)
        {
            var entity = await _db.Set<Certificate>().FirstOrDefaultAsync(c => c.CertificateId == certificateId);
            if (entity is null) throw new KeyNotFoundException("Certificate not found.");

            entity.CertificateUrl = dto.CertificateUrl;
            _db.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid certificateId)
        {
            var entity = await _db.Set<Certificate>().FirstOrDefaultAsync(c => c.CertificateId == certificateId);
            if (entity is null) return;

            _db.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<CertificateDetailsDto?> GetByIdAsync(Guid certificateId)
        {
            var entity = await _db.Set<Certificate>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CertificateId == certificateId);

            return entity is null ? null : _mapper.Map<CertificateDetailsDto>(entity);
        }

        public async Task<List<CertificateListItemDto>> ListByUserAsync(Guid userId)
        {

            var query = _db.Set<Certificate>()
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.IssuedAt)
                .ProjectTo<CertificateListItemDto>(_mapper.ConfigurationProvider);

            return await query.ToListAsync();
        }

        //public async Task<PagedResult<CertificateListItemDto>> SearchAsync(CertificateQueryDto query)
        //{
        //    var q = _db.Set<Certificate>().AsNoTracking().AsQueryable();

        //    if (query.UserId.HasValue) q = q.Where(c => c.UserId == Guid.Parse(query.UserId.Value.ToString()));
        //    if (query.CourseId.HasValue) q = q.Where(c => c.CourseId == Guid.Parse(query.CourseId.Value.ToString()));
        //    if (query.IssuedFrom.HasValue) q = q.Where(c => c.IssuedAt >= query.IssuedFrom.Value);
        //    if (query.IssuedTo.HasValue) q = q.Where(c => c.IssuedAt <= query.IssuedTo.Value);

        //    // Sorting
        //    var sort = (query.SortBy ?? "issuedAt").ToLowerInvariant();
        //    var desc = query.Desc;

        //    q = sort switch
        //    {
        //        "courseid" => desc ? q.OrderByDescending(c => c.CourseId) : q.OrderBy(c => c.CourseId),
        //        "userid" => desc ? q.OrderByDescending(c => c.UserId) : q.OrderBy(c => c.UserId),
        //        _ => desc ? q.OrderByDescending(c => c.IssuedAt) : q.OrderBy(c => c.IssuedAt)
        //    };

        //    var total = await q.CountAsync();
        //    var page = Math.Max(1, query.Page);
        //    var size = Math.Clamp(query.PageSize, 1, 200);

        //    var items = await q
        //        .Skip((page - 1) * size)
        //        .Take(size)
        //        .ProjectTo<CertificateListItemDto>(_mapper.ConfigurationProvider)
        //        .ToListAsync();

        //    return new PagedResult<CertificateListItemDto>
        //    {
        //        Items = items,
        //        Page = page,
        //        PageSize = size,
        //        Total = total
        //    };
        //}

        public async Task<CertificateVerifyResultDto> VerifyByUrlAsync(string certificateUrl)
        {
            var cert = await _db.Set<Certificate>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CertificateUrl == certificateUrl);

            if (cert is null)
                return new CertificateVerifyResultDto { Found = false, Message = "Not found" };

            return _mapper.Map<CertificateVerifyResultDto>(cert);
        }

        public async Task<CertificateVerifyResultDto> VerifyByEnrollmentAsync(Guid enrollmentId)
        {
            var cert = await _db.Set<Certificate>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.EnrollmentId == enrollmentId);

            if (cert is null)
                return new CertificateVerifyResultDto { Found = false, Message = "Not found" };

            return _mapper.Map<CertificateVerifyResultDto>(cert);
        }
    }
}
