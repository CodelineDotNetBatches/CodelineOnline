namespace ReportsManagements.DTOs
{
    public class AttendanceRecordUpdateDto
    {
        
            public DateTime? CheckIn { get; set; }
            public DateTime? CheckOut { get; set; }
            public string? Status { get; set; }   // Present, Absent, Late
            public string? ReviewStatus { get; set; } // Pending, Approved, Rejected
            public int? ReasonCodeId { get; set; }    // optional: Sick, Vacation...
            public int? GeolocationId { get; set; }   // in case we need to update location
            public double? FaceMatchScore { get; set; }
            public double? LivenessScore { get; set; }
        
    }
}
