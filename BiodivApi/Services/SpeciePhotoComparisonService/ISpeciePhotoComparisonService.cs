using System.Threading.Tasks;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;

namespace BiodivApi.Services.SpeciePhotoComparisonService
{
    public interface ISpeciePhotoComparisonService
    {
        Task<Specie> GetMostSimilarSpecie(SpecieIdentificationDto identificationDto);
    }
}