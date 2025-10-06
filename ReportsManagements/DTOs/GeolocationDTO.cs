namespace ReportsManagements.DTOs
{
    public class GeolocationDTO { 
    
        
        public class GeolocationCreateDto
        {
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public decimal RediusMeters { get; set; }
            public int BranchId { get; set; }
        }

        public class GeolocationResponseDto
        {
            public int GeolocationId { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public decimal RediusMeters { get; set; }
            public bool IsActive { get; set; }
            public int BranchId { get; set; }
        }

        public class GeoRadiusUpdateDto
        {
            public double NewRadius { get; set; }
        }
    }

}




