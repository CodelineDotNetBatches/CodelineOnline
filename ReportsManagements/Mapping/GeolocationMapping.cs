using AutoMapper;
using ReportsManagements.DTOs;
using ReportsManagements.Models;

namespace ReportsManagements.Mapping
{
    public class GeolocationMapping: Profile
    {
        public GeolocationMapping()
        {
            CreateMap<GeolocationDTO.GeolocationCreateDto, Geolocation>();
            CreateMap<Geolocation, GeolocationDTO.GeolocationResponseDto>();
        }
    }
}
