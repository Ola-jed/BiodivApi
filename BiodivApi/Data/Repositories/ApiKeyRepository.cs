using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Data.Context;
using BiodivApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiodivApi.Data.Repositories
{
    public class ApiKeyRepository: GenericRepository<ApiKey>,IApiKeyRepository
    {
        public ApiKeyRepository(BiodivDbContext dbContext) : base(dbContext) { }

        public async Task<ApiKey> GetWithKey(string key)
        {
            return await GetAll().FirstOrDefaultAsync(a => a.EncodedKey == key);
        }
    }
}