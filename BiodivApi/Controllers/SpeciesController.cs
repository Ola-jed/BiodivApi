using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiodivApi.Attributes;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;
using BiodivApi.Services.PaginatorService;
using BiodivApi.Services.SpeciesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiodivApi.Controllers
{
    [Route("api/[controller]")]
    [ApiKey]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpecieService _specieService;
        private readonly Dictionary<string, Func<string, Task<IEnumerable<SpecieReadDto>>>> _search;

        public SpeciesController(ISpecieService specieService)
        {
            _specieService = specieService;
            _search = new Dictionary<string, Func<string, Task<IEnumerable<SpecieReadDto>>>>
            {
                { "name", _specieService.FindByName },
                { "englishName", _specieService.FindByEnglishName },
                { "scientificName", _specieService.FindByScientificName },
                { "taxonomicGroup", _specieService.FindByTaxonomicGroup },
                { "habitat", _specieService.FindByHabitat },
                { "localName", _specieService.FindByLocalName }
            };
        }

        [HttpGet]
        public async Task<IEnumerable<SpecieReadDto>> GetAll([FromQuery] int pageSize = 20, [FromQuery] int pageNumber = 1)
        {
            var paginator = new Paginator
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            return await _specieService.GetAndPaginate(paginator);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}",Name = "Get")]
        public async Task<ActionResult> Get(int id)
        {
            var specie = await _specieService.GetSpecie(id);
            return specie == null ? NotFound() : Ok(specie);
        }

        [HttpGet("specie-of-the-day")]
        public async Task<Specie> GetSpecieOfTheDay()
        {
            return await _specieService.GetRandomSpecie();
        }

        [HttpGet("search")]
        public async Task<IEnumerable<SpecieReadDto>> SearchByName([FromQuery] SpecieSearchDto searchDto)
        {
            return !_search.ContainsKey(searchDto.SearchCriteria)
                ? Enumerable.Empty<SpecieReadDto>()
                : await _search[searchDto.SearchCriteria](searchDto.Search);
        }
    }
}