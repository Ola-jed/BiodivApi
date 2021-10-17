using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiodivApi.Attributes;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;
using BiodivApi.Services.CacheService;
using BiodivApi.Services.PaginatorService;
using BiodivApi.Services.SpeciesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BiodivApi.Controllers
{
    [Route("api/[controller]")]
    [ApiKey]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpecieService _specieService;
        private readonly Dictionary<string, Func<string, Task<IEnumerable<SpecieReadDto>>>> _search;
        private readonly ICacheService _cacheService;

        public SpeciesController(ISpecieService specieService,
            ICacheService cacheService)
        {
            _specieService = specieService;
            _cacheService = cacheService;
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
        public async Task<IEnumerable<SpecieReadDto>> GetAll([FromQuery] int pageSize = 20,
            [FromQuery] int pageNumber = 1)
        {
            var paginator = new Paginator
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            var key = $"{nameof(GetAll)}-pageSize:{pageSize}-pageNumber:{pageNumber}";
            Task<IEnumerable<SpecieReadDto>> Func() => _specieService.GetAndPaginate(paginator);
            return await _cacheService.GetOrSet(key, Func);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}", Name = "Get")]
        public async Task<ActionResult> Get(int id)
        {
            var key = $"{nameof(Get)}-id:{id}";
            async Task<Specie> Func() => await _specieService.GetSpecie(id);
            var specie = await _cacheService.GetOrSet(key, Func);
            return specie == null ? NotFound() : Ok(specie);
        }

        [HttpGet("specie-of-the-day")]
        public async Task<Specie> GetSpecieOfTheDay()
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            return await _cacheService.GetOrSet(nameof(GetSpecieOfTheDay), _specieService.GetRandomSpecie,
                cacheOptions);
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