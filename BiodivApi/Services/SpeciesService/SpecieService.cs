using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BiodivApi.Data.Dtos;
using BiodivApi.Data.Repositories;
using BiodivApi.Entities;
using BiodivApi.Services.PaginatorService;
using Microsoft.EntityFrameworkCore;

namespace BiodivApi.Services.SpeciesService
{
    public class SpecieService : ISpecieService
    {
        private readonly ISpecieRepository _specieRepository;
        private readonly ILocalNameRepository _localNameRepository;
        private readonly IMapper _mapper;

        public SpecieService(ISpecieRepository specieRepository,
            ILocalNameRepository localNameRepository,
            IMapper mapper)
        {
            _specieRepository = specieRepository;
            _localNameRepository = localNameRepository;
            _mapper = mapper;
        }

        public async Task<Specie> CreateSpecie(SpecieCreateDto specieCreateDto)
        {
            var specieEntity = _mapper.Map<Specie>(specieCreateDto);
            await _specieRepository.Create(specieEntity);
            await _specieRepository.SaveChanges();
            return specieEntity;
        }

        public async Task<IEnumerable<SpecieReadDto>> GetAndPaginate(Paginator paginator)
        {
            return _mapper.Map<IEnumerable<SpecieReadDto>>(await _specieRepository.GetSpecies(paginator));
        }

        public async Task<Specie> GetRandomSpecie()
        {
            return await _specieRepository.GetRandom();
        }

        public async Task<IEnumerable<SpecieReadDto>> FindByName(string name)
        {
            var species = await _specieRepository.Find(s => EF.Functions.Like(s.Name, $"%{name}%"));
            return _mapper.Map<IEnumerable<SpecieReadDto>>(species);
        }

        public async Task<IEnumerable<SpecieReadDto>> FindByLocalName(string localName)
        {
            var ids = await _localNameRepository.GetSpeciesIdWithSpelling(localName);
            return _mapper.Map<IEnumerable<SpecieReadDto>>(await _specieRepository.Find(s => ids.Contains(s.Id)));
        }

        public async Task<IEnumerable<SpecieReadDto>> FindByEnglishName(string englishName)
        {
            var species = await _specieRepository.Find(s => EF.Functions.Like(s.EnglishName, $"%{englishName}%"));
            return _mapper.Map<IEnumerable<SpecieReadDto>>(species);
        }

        public async Task<IEnumerable<SpecieReadDto>> FindByScientificName(string scientificName)
        {
            var species = await _specieRepository.Find(s => EF.Functions.Like(s.ScientificName, $"%{scientificName}%"));
            return _mapper.Map<IEnumerable<SpecieReadDto>>(species);
        }

        public async Task<IEnumerable<SpecieReadDto>> FindByTaxonomicGroup(string taxonomicGroup)
        {
            var species = await _specieRepository.Find(s => EF.Functions.Like(s.TaxonomicGroup, $"%{taxonomicGroup}%"));
            return _mapper.Map<IEnumerable<SpecieReadDto>>(species);
        }

        public async Task<IEnumerable<SpecieReadDto>> FindByHabitat(string habitat)
        {
            var species = await _specieRepository.Find(s => EF.Functions.Like(s.Habitat, $"%{habitat}%"));
            return _mapper.Map<IEnumerable<SpecieReadDto>>(species);
        }

        public async Task<Specie> GetSpecie(int id)
        {
            return await _specieRepository.GetSpecie(id);
        }

        public async Task UpdateSpecie(Specie specieToUpdate, SpecieUpdateDto specie)
        {
            _mapper.Map(specie, specieToUpdate);
            _specieRepository.Update(specieToUpdate);
            await _specieRepository.SaveChanges();
        }

        public async Task DeleteSpecie(Specie specie)
        {
            _specieRepository.Delete(specie);
            await _specieRepository.SaveChanges();
        }
    }
}