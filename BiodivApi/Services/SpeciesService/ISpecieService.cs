using System.Collections.Generic;
using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;
using BiodivApi.Services.PaginatorService;

namespace BiodivApi.Services.SpeciesService
{
    public interface ISpecieService
    {
        Task<Specie> CreateSpecie(SpecieCreateDto specieCreateDto);
        Task<IEnumerable<SpecieReadDto>> GetAndPaginate(Paginator paginator);
        Task<Specie> GetRandomSpecie();
        Task<IEnumerable<SpecieReadDto>> FindByName(string name);
        Task<IEnumerable<SpecieReadDto>> FindByLocalName(string localName);
        Task<IEnumerable<SpecieReadDto>> FindByEnglishName(string englishName);
        Task<IEnumerable<SpecieReadDto>> FindByScientificName(string scientificName);
        Task<IEnumerable<SpecieReadDto>> FindByTaxonomicGroup(string taxonomicGroup);
        Task<IEnumerable<SpecieReadDto>> FindByHabitat(string habitat);
        Task<Specie> GetSpecie(int id);
        Task UpdateSpecie(Specie specieToUpdate, SpecieUpdateDto specie);
        Task DeleteSpecie(Specie specie);
    }
}