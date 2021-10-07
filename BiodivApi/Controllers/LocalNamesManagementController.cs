using System.Threading.Tasks;
using BiodivApi.Attributes;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;
using BiodivApi.Entities.Enums;
using BiodivApi.Services.LocalNamesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiodivApi.Controllers
{
    [Route("api/")]
    [ApiKey(ApiKeyPermission.All)]
    [ApiController]
    public class LocalNamesManagementController : ControllerBase
    {
        private readonly ILocalNamesService _localNamesService;

        public LocalNamesManagementController(ILocalNamesService localNamesService)
        {
            _localNamesService = localNamesService;
        }

        [HttpPost("Species/{specieId:int}/LocalNames")]
        public async Task<LocalName> CreateLocalName(int specieId,LocalNameCreateDto nameCreateDto)
        {
            var localName = await _localNamesService.CreateLocalName(specieId, nameCreateDto);
            return localName;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("[controller]/{id:int}")]
        public async Task<ActionResult> Put(int id,LocalNameUpdateDto nameUpdateDto)
        {
            var localName = await _localNamesService.GetLocalName(id);
            if (localName == null)
            {
                return NotFound();
            }

            await _localNamesService.UpdateLocalName(localName,nameUpdateDto);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("[controller]/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var localName = await _localNamesService.GetLocalName(id);
            if (localName == null)
            {
                return NotFound();
            }

            await _localNamesService.DeleteLocalName(localName);
            return NoContent();
        }
    }
}