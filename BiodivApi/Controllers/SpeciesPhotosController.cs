using System.Collections.Generic;
using System.Threading.Tasks;
using BiodivApi.Attributes;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;
using BiodivApi.Services.PaginatorService;
using BiodivApi.Services.SpeciesPhotosService;
using Microsoft.AspNetCore.Mvc;

namespace BiodivApi.Controllers
{
    [Route("api/[controller]")]
    [ApiKey]
    [ApiController]
    public class SpeciesPhotosController : ControllerBase
    {
        private readonly ISpeciePhotoService _speciePhotoService;

        public SpeciesPhotosController(ISpeciePhotoService speciePhotoService)
        {
            _speciePhotoService = speciePhotoService;
        }

        [HttpGet]
        public async Task<IEnumerable<SpecieWithPhotosReadDto>> GetAllPhotos(
            [FromQuery] int pageSize = 20,
            [FromQuery] int pageNumber = 1)
        {
            var paginator = new Paginator
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            return await _speciePhotoService.GetPhotosWithSpecie(paginator);
        }

        [HttpGet("photo-of-the-day")]
        public async Task<SpeciePhoto> GetPhotoOfTheDay()
        {
            return await _speciePhotoService.GetRandomPhoto();
        }
    }
}