namespace ReportsManagements.Services
{
    public class FileCodeService : IFileCodeService
    {
        private readonly string[] allowedExtensions = { ".jpg", ".png", ".pdf", ".docx" };
        private const long MaxSizeInBytes = 5 * 1024 * 1024; // 5 MB
        // Validate file by extension, size, and optional content check
        public bool IsValidFile(string fileName, long fileSize, byte[]? content = null)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            if (!allowedExtensions.Contains(extension) || fileSize > MaxSizeInBytes)
                return false;

            // MIME sniff
            if (content != null && content.Length > 0)
            {
                var firstBytes = content.Take(4).ToArray();
                // check PDF starts with "%PDF"
                if (extension == ".pdf" && !(firstBytes[0] == 0x25 && firstBytes[1] == 0x50))
                    return false;
            }

            return true;
        }
    }
}
