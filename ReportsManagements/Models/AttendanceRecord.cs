namespace ReportsManagements.Models
{
    public class AttendanceRecord
    {
        public int AttId { get; set; }
        public int SessionId { get; set; }
        public int StudentId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Status { get; set; } // e.g., Present, Absent, Late
        public int ReasonCodeId { get; set; } // e.g., Sick, Vacation
        public int CapturedPhotoId { get; set; } // Reference to photo
        //FaceMatchScore
        public double FaceMatchScore { get; set; } // Confidence score for face recognition
        //LivenessScore, GeolocationId, ReviewStatus, CreatedAt, CreatedBy, UploadedAt, UploadedBy.
        public double LivenessScore { get; set; } // Confidence score for liveness detection
        public int GeolocationId { get; set; } // Reference to geolocation data
        public string ReviewStatus { get; set; } // e.g., Pending, Approved, Rejected
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTime UploadedAt { get; set; }
        public string  UploadedBy { get; set; }


    }
}
