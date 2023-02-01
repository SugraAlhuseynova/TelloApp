using Microsoft.AspNetCore.Http;

namespace Tello.Api.Helpers
{
    public static class FileManager
    {
        public static string Save(string folder, string root, IFormFile file)
        {
            string newFileImage = Guid.NewGuid().ToString() + (file.FileName.Length > 64 ? file.FileName.Substring(file.FileName.Length - 64, 64) : file.FileName);
            string path = Path.Combine(root, folder, newFileImage);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return newFileImage;
        }
        public static void Delete(string root, string folder, string image)
        {
            string path = Path.Combine(root, folder, image);
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
