namespace ReportsManagements.DTOs
{
    public class AttendanceRecordCreateDto
    {
     
            public int SessionId { get; set; }
            public int StudentId { get; set; }

            public DateTime CheckIn { get; set; }
            public DateTime? CheckOut { get; set; }

            public string? Status { get; set; }   // Present, Absent, Late
            public string? ReviewStatus { get; set; } = "Pending"; // default = Pending

            public int? ReasonCodeId { get; set; }
            public int? GeolocationId { get; set; }

            public double? FaceMatchScore { get; set; }
            public double LivenessScore { get; set; }

            public string CreatedBy { get; set; }
            public string? UploadedBy { get; set; }
        public string IdempotencyKey { get; set; }
    }
}
