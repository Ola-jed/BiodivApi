using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Entities;

namespace BiodivApi.Data.Repositories
{
    public interface IApiKeyRepository: IRepository<ApiKey>
    {
        Task<ApiKey> GetWithKey(string key);
    }
}