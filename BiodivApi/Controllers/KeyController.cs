using System.Threading.Tasks;
using BiodivApi.Entities.Enums;
using BiodivApi.Services.ApiKeyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BiodivApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyController : ControllerBase
    {
        private const string ApiSecret = "API-SECRET";
        private readonly IApiKeyService _apiKeyService;
        private readonly IConfiguration _configuration;

        public KeyController(IApiKeyService apiKeyService, IConfiguration configuration)
        {
            _apiKeyService = apiKeyService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var hasSecret = Request.Headers.TryGetValue(ApiSecret, out var secrets) &&
                            secrets.ToString() == _configuration.GetSection(ApiSecret).Value;
            return await _apiKeyService.CreateApiKey(hasSecret ? ApiKeyPermission.All : ApiKeyPermission.Read);
        }
    }
}