using Azure.Storage.Blobs;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Options;


namespace Application.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobStorage _blobStorage;
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(IOptions<BlobStorage> blobStorageOptions)
        {
            _blobStorage = blobStorageOptions.Value;
            _blobServiceClient = new BlobServiceClient(_blobStorage.BlobStorageConnection);
        }

        public async Task SaveFileToBlobStorageAsync(string containerName, string blobName, Stream fileStream)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(fileStream, overwrite: true);
        }
    }
}
