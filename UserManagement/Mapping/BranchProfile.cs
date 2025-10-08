// MappingProfiles/BranchProfile.cs
using AutoMapper;
using System.Linq;
using UserManagement.DTOs;
using UserManagement.Models;

namespace UserManagement.MappingProfiles
{
    public class BranchProfile : Profile
    {
        public BranchProfile()
        {
            // Entity →  DTO
            CreateMap<Branch, BranchDTO>()
                .ForMember(dest => dest.PhoneNumbers,
                    opt => opt.MapFrom(src =>
                        (src.branchPNs ?? new List<BranchPN>()).Select(p => p.PhoneNumber)))
                .ForMember(dest => dest.BranchName,
                    opt => opt.MapFrom(src => src.BranchName));

            //  DTO → Entity
            CreateMap<BranchDTO, Branch>()
                 .ForMember(dest => dest.branchPNs,
                     opt => opt.MapFrom(src =>
                         (src.PhoneNumbers ?? new List<int>())
                             .Distinct()
                             .Select(p => new BranchPN { PhoneNumber = p })
                             .ToList()
                     ));

            // Update DTO → Entity
            CreateMap<BranchDTO, Branch>()
                .ForMember(dest => dest.branchPNs,
                    opt => opt.MapFrom(src => src.PhoneNumbers != null
                        ? src.PhoneNumbers.Select(p => new BranchPN { PhoneNumber = p }).ToList()
                        : new List<BranchPN>()));
        }
    }
}
