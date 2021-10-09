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
using SixLabors.ImageSharp.Processing;

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
            var tempLocation = await _storageService.Save(identificationDto.Photo, TempFolder);
            var speciePhotos = await _speciePhotoRepository.GetSpeciePhotos();
            var data = speciePhotos.Select(speciePhoto => new
            {
                SpeciePhoto = speciePhoto,
                DeltaE = GetTotalDeltaE(tempLocation, speciePhoto).Result
            }).ToList();
            var selectedSpecieId = data.OrderBy(v => v.DeltaE).First().SpeciePhoto.SpecieId;
            await _storageService.Delete(tempLocation);
            return await _specieRepository.GetById(selectedSpecieId);
        }

        private static async Task<double> GetTotalDeltaE(string tempImageLocation,
            SpeciePhoto photo,
            DeltaEAlgorithm deltaEAlgorithm = DeltaEAlgorithm.DeltaE76)
        {
            var img = await Image.LoadAsync<Rgb24>(tempImageLocation);
            var comparisonImage = await Image.LoadAsync<Rgb24>(photo.Photo);
            var minWidth = Math.Min(img.Width, comparisonImage.Width);
            var minHeight = Math.Min(img.Height, comparisonImage.Height);
            img.Mutate(i => i.Resize(minWidth,minHeight));
            comparisonImage.Mutate(i => i.Resize(minWidth,minHeight));
            double delta = 0;
            for (var i = 0; i < minWidth; i++)
            {
                for (var j = 0; j < minHeight; j++)
                {
                    var c = img[i, j];
                    var v = comparisonImage[i, j];
                    delta += deltaEAlgorithm switch
                    {
                        DeltaEAlgorithm.DeltaE94 => c.Rgb24ToRgb().ToLab().DeltaE94(v.Rgb24ToRgb().ToLab()),
                        DeltaEAlgorithm.DeltaE76 => c.Rgb24ToRgb().ToLab().DeltaE76(v.Rgb24ToRgb().ToLab()),
                        _ => throw new ArgumentOutOfRangeException(nameof(deltaEAlgorithm), deltaEAlgorithm, null)
                    };
                }
            }
            return delta;
        }
    }
}