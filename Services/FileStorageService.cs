using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HariCKInventry.Services
{
    public class FileStorageService
    {
        private readonly string _stlRoot;
        private readonly string _photosRoot;

        public FileStorageService(IWebHostEnvironment env)
        {
            var wwwroot = Path.Combine(env.ContentRootPath, "wwwroot");
            _stlRoot = Path.Combine(wwwroot, "uploads", "stl");
            _photosRoot = Path.Combine(wwwroot, "uploads", "photos");

            Directory.CreateDirectory(_stlRoot);
            Directory.CreateDirectory(_photosRoot);
        }

        public async Task<string> SaveStlAsync(string originalFileName, Stream fileStream)
        {
            var ext = Path.GetExtension(originalFileName).ToLowerInvariant();
            if (ext != ".stl") throw new InvalidOperationException("Only .stl files are allowed.");

            var safeName = $"{Guid.NewGuid()}{ext}";
            var path = Path.Combine(_stlRoot, safeName);

            using var fs = new FileStream(path, FileMode.Create);
            await fileStream.CopyToAsync(fs);

            return $"/uploads/stl/{safeName}";
        }

        public async Task<string> SavePhotoAsync(string originalFileName, Stream fileStream)
        {
            var ext = Path.GetExtension(originalFileName).ToLowerInvariant();
            if (ext is not ".jpg" and not ".jpeg" and not ".png" and not ".webp")
                throw new InvalidOperationException("Only .jpg, .jpeg, .png, .webp photos are allowed.");

            var safeName = $"{Guid.NewGuid()}{ext}";
            var path = Path.Combine(_photosRoot, safeName);

            using var fs = new FileStream(path, FileMode.Create);
            await fileStream.CopyToAsync(fs);

            return $"/uploads/photos/{safeName}";
        }
    }
}