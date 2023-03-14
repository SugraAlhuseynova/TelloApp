using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.IServices.Storage.Azure;
using static System.Reflection.Metadata.BlobBuilder;

namespace Tello.Service.Apps.Admin.Implementations.Storage.Azure
{
    public class AzureStorage : IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }
        public async Task DeleteAsync(string container, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobClient client = _blobContainerClient.GetBlobClient(fileName);
            await client.DeleteAsync();
        }
        public bool HasFile(string container, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            return _blobContainerClient.GetBlobs().Any(b=>b.Name == fileName);
        }
        public async Task<string> SaveAsync(string container, IFormFile file)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            string newFileImage = Guid.NewGuid().ToString() + (file.FileName.Length > 64 ? file.FileName.Substring(file.FileName.Length - 64, 64) : file.FileName);

            BlobClient client = _blobContainerClient.GetBlobClient(newFileImage);

            await client.UploadAsync(file.OpenReadStream());
            return newFileImage;
        }
        public async Task RestoreAsync(string container, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobClient client = _blobContainerClient.GetBlobClient(fileName);
            await client.UndeleteAsync();
        }
        
    }
}
