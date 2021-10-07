using System.Threading.Tasks;
using AutoMapper;
using Biodiv.Entities;
using BiodivApi.Data.Dtos;
using BiodivApi.Data.Repositories;
using BiodivApi.Entities;

namespace BiodivApi.Services.LocalNamesService
{
    public class LocalNamesService: ILocalNamesService
    {
        private readonly IRepository<LocalName> _repository;
        private readonly IMapper _mapper;

        public LocalNamesService(IRepository<LocalName> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LocalName> GetLocalName(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<LocalName> CreateLocalName(int specieId, LocalNameCreateDto localNameCreateDto)
        {
            var localName = _mapper.Map<LocalName>(localNameCreateDto);
            localName.SpecieId = specieId;
            await _repository.Create(localName);
            await _repository.SaveChanges();
            return localName;
        }

        public async Task UpdateLocalName(LocalName localName, LocalNameUpdateDto localNameUpdateDto)
        {
            _mapper.Map(localNameUpdateDto, localName);
            _repository.Update(localName);
            await _repository.SaveChanges();
        }

        public async Task DeleteLocalName(LocalName localName)
        {
            _repository.Delete(localName);
            await _repository.SaveChanges();
        }
    }
}