using System.ComponentModel.DataAnnotations;

namespace ReportsManagements.Models
{
    // Model representing a file storage entity
    public class FileStorage 
    {
        [Key]
        public int FileStorageId { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }            
        public DateTime UploadedAt { get; set; }
        public string UploadedBy { get; set; }
    }
}
