using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.IServices.Storage;
using static System.Net.Mime.MediaTypeNames;

namespace Tello.Service.Apps.Admin.Implementations.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }
        public async Task DeleteAsync(string folder, string fileName)
        {
            await _storage.DeleteAsync(folder, fileName);   
        }

        public bool HasFile(string folder, string fileName)
        {
            return _storage.HasFile(folder, fileName);
        }

        public async Task RestoreAsync(string folder, string fileName)
        {
            await _storage.RestoreAsync(folder, fileName);
        }

        public async Task<string> SaveAsync(string folder, IFormFile file)
        {
            var data = await _storage.SaveAsync(folder, file);
            return data;
        }
    }
}
