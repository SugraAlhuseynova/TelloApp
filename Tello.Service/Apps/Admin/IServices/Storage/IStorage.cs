using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Apps.Admin.IServices.Storage
{
    public interface IStorage
    {
        public Task<string> SaveAsync(string folder, IFormFile file);
        public Task DeleteAsync(string folder, string fileName);
        public bool HasFile(string folder, string fileName); 
        public Task RestoreAsync(string folder, string fileName); 
    }
}
