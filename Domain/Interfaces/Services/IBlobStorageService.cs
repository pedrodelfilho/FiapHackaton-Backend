namespace Domain.Interfaces.Services
{
    public interface IBlobStorageService
    {
        Task SaveFileToBlobStorageAsync(string containerName, string blobName, Stream fileStream);
    }
}
