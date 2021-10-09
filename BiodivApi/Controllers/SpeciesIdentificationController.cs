using System.Threading.Tasks;
using BiodivApi.Attributes;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;
using BiodivApi.Services.SpeciePhotoComparisonService;
using Microsoft.AspNetCore.Mvc;

namespace BiodivApi.Controllers
{
    [Route("api/[controller]")]
    [ApiKey]
    [ApiController]
    public class SpeciesIdentificationController : ControllerBase
    {
        private readonly ISpeciePhotoComparisonService _speciePhotoComparisonService;

        public SpeciesIdentificationController(ISpeciePhotoComparisonService speciePhotoComparisonService)
        {
            _speciePhotoComparisonService = speciePhotoComparisonService;
        }

        [HttpPost]
        public async Task<Specie> GetCorrespondingSpecie([FromForm] SpecieIdentificationDto identificationDto)
        {
            var specie = await _speciePhotoComparisonService.GetMostSimilarSpecie(identificationDto);
            return specie;
        }
    }
}