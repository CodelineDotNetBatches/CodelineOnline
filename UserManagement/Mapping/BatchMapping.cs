using AutoMapper;
using UserManagement.Models;
using UserManagement.DTOs;

namespace UserManagement.Mapping
{
    /// <summary>
    /// AutoMapper profile for Batch ↔ BatchDTO mapping.
    /// </summary>
    public class BatchMapping : Profile
    {
        public BatchMapping()
        {
            //  Automatically handles both directions
            CreateMap<Batch, BatchDTO>().ReverseMap();
        }
    }
}
