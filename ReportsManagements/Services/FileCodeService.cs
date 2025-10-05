using System.IO;

namespace ReportsManagements.Services
{
    public class FileCodeService : IFileCodeService
    {
        // Validates file based on its name and size
        public bool IsValidFile(string fileName, long fileSize)
        {
            var allowedExtensions = new[] { ".jpg", ".png", ".pdf", ".docx" };
            var extension = Path.GetExtension(fileName).ToLower();

            const long maxSizeInBytes = 5 * 1024 * 1024; // 5 MB

            return allowedExtensions.Contains(extension) && fileSize <= maxSizeInBytes;
        }
    }
}
