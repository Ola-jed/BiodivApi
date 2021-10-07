using System.Threading.Tasks;
using BiodivApi.Attributes;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities.Enums;
using BiodivApi.Services.SpeciesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiodivApi.Controllers
{
    [Route("api/[controller]")]
    [ApiKey(ApiKeyPermission.All)]
    [ApiController]
    public class SpeciesManagementController : ControllerBase
    {
        private readonly ISpecieService _specieService;

        public SpeciesManagementController(ISpecieService specieService)
        {
            _specieService = specieService;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult> Post(SpecieCreateDto specieCreateDto)
        {
            var specie = await _specieService.CreateSpecie(specieCreateDto);
            return CreatedAtRoute(nameof(SpeciesController.Get),new {specie.Id},specie);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, SpecieUpdateDto specieUpdateDto)
        {
            var specie = await _specieService.GetSpecie(id);
            if (specie == null)
            {
                return NotFound();
            }
            await _specieService.UpdateSpecie(specie, specieUpdateDto);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var specie = await _specieService.GetSpecie(id);
            if (specie == null)
            {
                return NotFound();
            }

            await _specieService.DeleteSpecie(specie);
            return NoContent();
        }
    }
}