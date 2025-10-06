using AutoMapper;
using ReportsManagements.DTOs;
using ReportsManagements.Models;

namespace ReportsManagements.Mapping
{
    public class GeoRadiusUpdateMapping: Profile
    {
        public GeoRadiusUpdateMapping() 
        {
            CreateMap<GeolocationDTO.GeoRadiusUpdateDto, Geolocation>()
               .ForMember(dest => dest.RediusMeters, opt => opt.MapFrom(src => (decimal)src.NewRadius));
        }
    }
}
