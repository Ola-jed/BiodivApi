using System.Threading.Tasks;
using BiodivApi.Attributes;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities.Enums;
using BiodivApi.Services.LocalDistributionsService;
using Microsoft.AspNetCore.Mvc;

namespace BiodivApi.Controllers
{
    [Route("api/")]
    [ApiKey(ApiKeyPermission.All)]
    [ApiController]
    public class LocalDistributionsManagementController : ControllerBase
    {
        private readonly ILocalDistributionsService _distributionsService;

        public LocalDistributionsManagementController(ILocalDistributionsService distributionsService)
        {
            _distributionsService = distributionsService;
        }

        [HttpPost("Species/{specieId:int}/LocalDistributions")]
        public async Task<ActionResult> UploadLocalDistribution(int specieId,
            [FromForm] LocalDistributionCreateDto distributionCreateDto)
        {
            var localDistribution = await _distributionsService.CreateLocalDistribution(specieId,distributionCreateDto);
            return Ok(localDistribution);
        }

        [HttpPut("[controller]/{id:int}")]
        public async Task<ActionResult> Put(int id,[FromForm]LocalDistributionUpdateDto distributionUpdateDto)
        {
            var localDistribution = await _distributionsService.GetLocalDistribution(id);
            if (localDistribution == null)
            {
                return NotFound();
            }
            await _distributionsService.UpdateLocalDistribution(localDistribution, distributionUpdateDto);
            return NoContent();
        }

        [HttpDelete("[controller]/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var localDistribution = await _distributionsService.GetLocalDistribution(id);
            if (localDistribution == null)
            {
                return NotFound();
            }

            await _distributionsService.DeleteLocalDistribution(localDistribution);
            return NoContent();
        }
    }
}