namespace ReportsManagements.Services
{
    public interface IGeoValidationService
    {
        bool IsPointInsideCircle(double PoinLat, double PointLon, double CenterLat, double CenterLon, double RadiusMeters, out double DistanceMeters);
    }
}