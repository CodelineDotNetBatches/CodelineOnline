namespace ReportsManagements.Services
{
    public interface IFileCodeService
    {
        bool IsValidFile(string fileName, long fileSize, byte[]? content = null);
    }
}