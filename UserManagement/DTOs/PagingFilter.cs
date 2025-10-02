using UserManagement.Models;

namespace UserManagement.DTOs
{
    public sealed record PagingFilter(
        int Page = 1,
        int PageSize = 20,
        string? Expertise = null,
        ExperienceLevel? Level = null,
        TeachingStyle? Style = null
    );
}