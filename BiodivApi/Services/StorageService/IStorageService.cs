using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BiodivApi.Services.StorageService
{
    public interface IStorageService
    {
        Task<string> Save(IFormFile file,string baseDirectory);
        Task<Stream> GetStream(string path);
        Task Delete(string path);
    }
}