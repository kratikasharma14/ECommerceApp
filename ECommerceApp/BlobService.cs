using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace ECommerceApp.Services  // ⚠️ make sure this matches your project — see note below
{
    public class BlobService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobService(IConfiguration config)
        {
            var connectionString = config["AzureBlobStorage:ConnectionString"];
            var containerName = config["AzureBlobStorage:ContainerName"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            _containerClient.CreateIfNotExists(PublicAccessType.None); // private by default
        }

        // Upload a file, returns the blob name
        public async Task<string> UploadAsync(IFormFile file)
        {
            var blobName = $"{Guid.NewGuid()}-{file.FileName}";
            var blobClient = _containerClient.GetBlobClient(blobName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

            return blobName;
        }

        // Generate a temporary read URL (like the SAS you made manually)
        public string GetReadUrl(string blobName, int expiryMinutes = 60)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = _containerClient.Name,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(expiryMinutes)
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            return blobClient.GenerateSasUri(sasBuilder).ToString();
        }

        // Delete a file
        public async Task DeleteAsync(string blobName)
        {
            await _containerClient.GetBlobClient(blobName).DeleteIfExistsAsync();
        }
    }
}