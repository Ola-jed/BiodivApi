using System.Threading.Tasks;
using BiodivApi.Attributes;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;
using BiodivApi.Entities.Enums;
using BiodivApi.Services.SpeciesPhotosService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiodivApi.Controllers
{
    [Route("api")]
    [ApiKey(ApiKeyPermission.All)]
    [ApiController]
    public class SpeciesPhotosManagementController: ControllerBase
    {
        private readonly ISpeciePhotoService _photoService;

        public SpeciesPhotosManagementController(ISpeciePhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost("Species/{specieId:int}/SpeciePhotos")]
        public async Task<SpeciePhoto> UploadPhoto(int specieId,[FromForm]SpeciePhotoCreateDto photoCreateDto)
        {
            var speciePhoto = await _photoService.CreateSpeciePhoto(specieId, photoCreateDto);
            return speciePhoto;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("[controller]/{id:int}")]
        public async Task<ActionResult> Put(int id,[FromForm]SpeciePhotoUpdateDto photoUpdateDto)
        {
            var photo = await _photoService.GetPhoto(id);
            if (photo == null)
            {
                return NotFound();
            }
            await _photoService.UpdateSpeciePhoto(photo, photoUpdateDto);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("[controller]/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var photo = await _photoService.GetPhoto(id);
            if (photo == null)
            {
                return NotFound();
            }

            await _photoService.DeleteSpeciePhoto(photo);
            return NoContent();
        }
    }
}