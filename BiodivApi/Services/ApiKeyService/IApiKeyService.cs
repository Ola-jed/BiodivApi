using System.Threading.Tasks;
using BiodivApi.Entities.Enums;

namespace BiodivApi.Services.ApiKeyService
{
    public interface IApiKeyService
    {
        Task<string> CreateApiKey(ApiKeyPermission permission = ApiKeyPermission.Read);
        Task<bool> VerifyApiKey(string key);
        Task<bool> VerifyPermissions(string key,ApiKeyPermission permission);
    }
}