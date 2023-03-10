using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection.Emit;

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
        public static void Remove(string root, string folder, string image)
        {
            string path = Path.Combine(root, folder, image);
            if (File.Exists(path))
                File.Delete(path);
        }
        public static void ChangeFolder(string root, string folder, string prevFolder, string image)
        {
            string pathNew = Path.Combine(root, folder);
            string pathPrev = Path.Combine(root, prevFolder);
            if (Directory.Exists(pathPrev))
            {
                foreach (var file in new DirectoryInfo(pathPrev).GetFiles())
                {
                    if (file.Name == image)
                        file.MoveTo(Path.Combine(pathNew, file.Name));
                }
            }
        }
    }
}
