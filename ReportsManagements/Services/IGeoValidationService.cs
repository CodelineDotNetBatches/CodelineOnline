namespace ReportsManagements.Services
{
    public interface IGeoValidationService
    {
        //Main method to check if a point is within a circle
        bool IsPointInsideCircle(double PoinLat, double PointLon, double CenterLat, double CenterLon, double RadiusMeters, out double DistanceMeters);
    }
}