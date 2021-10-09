using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BiodivApi.Services.StorageService
{
    public class LocalStorageService: IStorageService
    {
        private readonly IWebHostEnvironment _env;

        public LocalStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> Save(IFormFile file,string baseDirectory)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = DateTime.Now.Ticks + extension;
            var pathBuilt = Path.Combine(_env.WebRootPath, baseDirectory);
            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }
            var path = Path.Combine(pathBuilt, fileName);
            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"wwwroot/{baseDirectory}/{fileName}";
        }

        public Task<Stream> GetStream(string path)
        {
            return Task.FromResult<Stream>(File.OpenRead(path));
        }

        public Task Delete(string path)
        {
            File.Delete(path);
            return Task.CompletedTask;
        }
    }
}