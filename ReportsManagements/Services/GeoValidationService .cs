namespace ReportsManagements.Services
{
    public class GeoValidationService
    {
        //Main method to check if a point is within a circle
        public bool IsPointInsideCircle(double PoinLat, double PointLon, double CenterLat, double CenterLon, double RadiusMeters, out double DistanceMeters)//use out to return an extra value alongside the boolean result.
        {
            // Calculate the distance between the point and the circle center using Haversine formula
            DistanceMeters = HaversineDistance(PoinLat, PointLon, CenterLat, CenterLon);
            // Return true if the distance is less than or equal to the radius, false otherwise
            return DistanceMeters <= RadiusMeters;
        }
        //private helpers methods to calculate Haversine distance between two points on erth using formula
        private double HaversineDistance(double Lat1,double lon1,double Lat2,double Lon2)
        {
            double R = 6371000; // Radius of the Earth in meters
            double dLat = DegreesToRadians(Lat2 - Lat1);//difference in latitude in radians
            double dLon = DegreesToRadians(Lon2 - lon1);// difference in longitude in radians
            //Haversine formula
            var a =Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRadians(Lat1)) * Math.Cos(DegreesToRadians(Lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // Distance in meters
        }
        // Helper method to convert degrees to radians
        private double DegreesToRadians(double degrees) => degrees * Math.PI / 180;
    }
}
