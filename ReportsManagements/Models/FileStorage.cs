using System.ComponentModel.DataAnnotations;

namespace ReportsManagements.Models
{
    public class FileStorage
    {
        [Key]
        public int FileStorageId { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string Url { get; set; }            
        public DateTime UploadedAt { get; set; }
        [Required]
        public string UploadedBy { get; set; }
    }
}
