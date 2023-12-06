namespace SynapseAviationApp.Server.Services.Interfaces
{
    public interface IFileSystemService
    {
        Task<string> GetFileContentAsync(string fileName);
    }
}
