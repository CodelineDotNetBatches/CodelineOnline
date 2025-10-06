namespace ReportsManagements.Services
{
    public interface IFileCodeService
    {
        // Validate file by extension, size, and optional content check
        bool IsValidFile(string fileName, long fileSize, byte[]? content = null);
    }
}