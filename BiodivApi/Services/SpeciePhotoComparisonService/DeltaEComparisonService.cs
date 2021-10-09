using System;
using System.Linq;
using System.Threading.Tasks;
using BiodivApi.Data.Dtos;
using BiodivApi.Data.Repositories;
using BiodivApi.Entities;
using BiodivApi.Services.StorageService;
using BiodivImageComparison;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BiodivApi.Services.SpeciePhotoComparisonService
{
    public class DeltaEComparisonService : ISpeciePhotoComparisonService
    {
        private const string TempFolder = "Temp";
        private readonly ISpeciePhotoRepository _speciePhotoRepository;
        private readonly ISpecieRepository _specieRepository;
        private readonly IStorageService _storageService;

        public DeltaEComparisonService(ISpeciePhotoRepository speciePhotoRepository,
            IStorageService storageService,
            ISpecieRepository specieRepository)
        {
            _speciePhotoRepository = speciePhotoRepository;
            _storageService = storageService;
            _specieRepository = specieRepository;
        }

        public async Task<Specie> GetMostSimilarSpecie(SpecieIdentificationDto identificationDto)
        {
            var speciePhotos = await _speciePhotoRepository.GetSpeciePhotos();
            var data = speciePhotos.Select(async speciePhoto => new
            {
                SpeciePhoto = speciePhoto,
                DeltaE = await GetTotalDeltaE(identificationDto, speciePhoto)
            }).ToList();
            var selectedSpecie = (await data.OrderByDescending(async v => (await v).DeltaE).First()).SpeciePhoto;
            return await _specieRepository.GetById(selectedSpecie.SpecieId);
        }

        private async Task<double> GetTotalDeltaE(SpecieIdentificationDto identificationDto,
            SpeciePhoto photo,
            DeltaEAlgorithm deltaEAlgorithm = DeltaEAlgorithm.DeltaE76)
        {
            var tempImageLocation = await _storageService.Save(identificationDto.Photo, TempFolder);
            var img = await Image.LoadAsync<Rgb24>(tempImageLocation);
            var otherImage = await Image.LoadAsync<Rgb24>(photo.Photo);
            double delta = 0;
            for (var i = 0; i < img.Width; i++)
            {
                for (var j = 0; j < img.Height; j++)
                {
                    var c = img[i, j];
                    var v = otherImage[i, j];
                    delta += deltaEAlgorithm switch
                    {
                        DeltaEAlgorithm.DeltaE94 => c.Rgb24ToRgb().ToLab().DeltaE94(v.Rgb24ToRgb().ToLab()),
                        DeltaEAlgorithm.DeltaE76 => c.Rgb24ToRgb().ToLab().DeltaE76(v.Rgb24ToRgb().ToLab()),
                        _ => throw new ArgumentOutOfRangeException(nameof(deltaEAlgorithm), deltaEAlgorithm, null)
                    };
                }
            }

            await _storageService.Delete(tempImageLocation);
            return delta;
        }
    }
}