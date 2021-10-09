using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BiodivApi.Data.Dtos;
using BiodivApi.Data.Repositories;
using BiodivApi.Entities;
using BiodivApi.Services.PaginatorService;
using BiodivApi.Services.StorageService;

namespace BiodivApi.Services.SpeciesPhotosService
{
    public class SpeciePhotoService : ISpeciePhotoService
    {
        private const string LocationFolder = "SpeciePhoto";
        private readonly ISpecieRepository _specieRepository;
        private readonly ISpeciePhotoRepository _speciePhotoRepository;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        public SpeciePhotoService(ISpecieRepository specieRepository,
            ISpeciePhotoRepository speciePhotoRepository,
            IStorageService storageService,
            IMapper mapper)
        {
            _specieRepository = specieRepository;
            _speciePhotoRepository = speciePhotoRepository;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<SpeciePhoto> CreateSpeciePhoto(int specieId,SpeciePhotoCreateDto photoCreateDto)
        {
            var savePath = await _storageService.Save(photoCreateDto.Photo, LocationFolder);
            var speciePhoto = new SpeciePhoto
            {
                SpecieId = specieId,
                Photo = savePath
            };
            await _speciePhotoRepository.Create(speciePhoto);
            await _speciePhotoRepository.SaveChanges();
            return speciePhoto;
        }

        public async Task UpdateSpeciePhoto(SpeciePhoto speciePhoto, SpeciePhotoUpdateDto photoUpdateDto)
        {
            await _storageService.Delete(speciePhoto.Photo);
            speciePhoto.Photo = await _storageService.Save(photoUpdateDto.Photo, LocationFolder);
            _speciePhotoRepository.Update(speciePhoto);
            await _specieRepository.SaveChanges();
        }

        public async Task DeleteSpeciePhoto(SpeciePhoto speciePhoto)
        {
            await _storageService.Delete(speciePhoto.Photo);
            _speciePhotoRepository.Delete(speciePhoto);
            await _speciePhotoRepository.SaveChanges();
        }

        public async Task<IEnumerable<SpecieWithPhotosReadDto>> GetPhotosWithSpecie(Paginator paginator)
        {
            var speciesAndPhotos = await _specieRepository.GetSpeciesWithPhotos(paginator);
            return _mapper.Map<IEnumerable<SpecieWithPhotosReadDto>>(speciesAndPhotos);
        }

        public async Task<SpeciePhoto> GetRandomPhoto()
        {
            return await _speciePhotoRepository.GetRandom();
        }

        public async Task<SpeciePhoto> GetPhoto(int id)
        {
            return await _speciePhotoRepository.GetById(id);
        }
    }
}