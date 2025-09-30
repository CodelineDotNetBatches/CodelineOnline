using System.ComponentModel.DataAnnotations;

namespace ReportsManagements.Models
{
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
