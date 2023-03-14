using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Service.Apps.Admin.IServices.Storage.Local;

namespace Tello.Service.Apps.Admin.Implementations.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _web;

        public LocalStorage(IWebHostEnvironment web)
        {
            _web = web;
        }
        public async Task DeleteAsync(string folder, string fileName)
        {
            string path = Path.Combine(_web.WebRootPath, folder, fileName);
            if (File.Exists(path))
                File.Delete(path);
        }

        public bool HasFile(string folder, string fileName)
        {
            return File.Exists(Path.Combine(_web.WebRootPath, folder, fileName));
        }

        public Task RestoreAsync(string folder, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveAsync(string folder, IFormFile file)
        {
            string newFileImage = Guid.NewGuid().ToString() + (file.FileName.Length > 64 ? file.FileName.Substring(file.FileName.Length - 64, 64) : file.FileName);
            string path = Path.Combine(_web.WebRootPath, folder, newFileImage);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return newFileImage;
        }
    }
}
