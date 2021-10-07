using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Data.Dtos;
using BiodivApi.Data.Repositories;
using BiodivApi.Entities;
using BiodivApi.Services.StorageService;

namespace BiodivApi.Services.LocalDistributionsService
{
    public class LocalDistributionsService : ILocalDistributionsService
    {
        private const string LocationFolder = "LocalDistribution";
        private readonly IRepository<LocalDistribution> _repository;
        private readonly IStorageService _storageService;

        public LocalDistributionsService(IRepository<LocalDistribution> repository,
            IStorageService storageService)
        {
            _repository = repository;
            _storageService = storageService;
        }

        public async Task<LocalDistribution> GetLocalDistribution(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<LocalDistribution> CreateLocalDistribution(int specieId,
            LocalDistributionCreateDto distributionCreateDto)
        {
            var savePath = await _storageService.Save(distributionCreateDto.Image, LocationFolder);
            var specieLocalDistribution = new LocalDistribution
            {
                SpecieId = specieId,
                Image = savePath,
                Place = distributionCreateDto.Place
            };
            await _repository.Create(specieLocalDistribution);
            await _repository.SaveChanges();
            return specieLocalDistribution;
        }

        public async Task UpdateLocalDistribution(LocalDistribution localDistribution,
            LocalDistributionUpdateDto updateDto)
        {
            await _storageService.Delete(localDistribution.Image);
            localDistribution.Image = await _storageService.Save(updateDto.Image, LocationFolder);
            localDistribution.Place = updateDto.Place;
            _repository.Update(localDistribution);
            await _repository.SaveChanges();
        }

        public async Task DeleteLocalDistribution(LocalDistribution localDistribution)
        {
            await _storageService.Delete(localDistribution.Image);
            _repository.Delete(localDistribution);
            await _repository.SaveChanges();
        }
    }
}