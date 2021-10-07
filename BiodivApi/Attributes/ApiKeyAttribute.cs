using System;
using System.Threading.Tasks;
using BiodivApi.Entities.Enums;
using BiodivApi.Services.ApiKeyService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BiodivApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute: Attribute,IAsyncActionFilter
    {
        private const string ApiKeyName = "X-API-Key";
        private IApiKeyService _apiKeyService;
        private readonly ApiKeyPermission _permission;

        public ApiKeyAttribute(ApiKeyPermission permission = ApiKeyPermission.Read)
        {
            _permission = permission;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyName, out var extractedApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var apiKey = extractedApiKey.ToString();
            _apiKeyService = context.HttpContext.RequestServices.GetService<IApiKeyService>();
            if (_apiKeyService == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }
            if (!await _apiKeyService.VerifyApiKey(apiKey) || (_permission >= ApiKeyPermission.Read &&
                                                               !await _apiKeyService.VerifyPermissions(apiKey,
                                                                   _permission)))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}