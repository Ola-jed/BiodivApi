using System;
using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Data.Repositories;
using BiodivApi.Entities;
using BiodivApi.Entities.Enums;
using BiodivApi.Extensions;

namespace BiodivApi.Services.ApiKeyService
{
    public class ApiKeyService: IApiKeyService
    {
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyService(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task<string> CreateApiKey(ApiKeyPermission permission = ApiKeyPermission.Read)
        {
            var key = Guid.NewGuid().ToString().Base64Encode();
            var apiKey = new ApiKey()
            {
                EncodedKey = key,
                Permission = permission,
                ExpirationDate = DateTime.Now.AddDays(2)
            };
            await _apiKeyRepository.Create(apiKey);
            await _apiKeyRepository.SaveChanges();
            return key;
        }

        public async Task<bool> VerifyApiKey(string key)
        {
            var apiKey = await _apiKeyRepository.GetWithKey(key);
            if (apiKey == null)
            {
                return false;
            }
            if (apiKey.ExpirationDate.CompareTo(DateTime.Now) >= 0)
            {
                return true;
            }
            _apiKeyRepository.Delete(apiKey);
            await _apiKeyRepository.SaveChanges();
            return false;
        }

        public async Task<bool> VerifyPermissions(string key, ApiKeyPermission permission)
        {
            var apiKey = await _apiKeyRepository.GetWithKey(key);
            return apiKey != null && apiKey.Permission >= permission;
        }
    }
}