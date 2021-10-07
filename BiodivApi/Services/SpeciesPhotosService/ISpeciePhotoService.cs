using System.Collections.Generic;
using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;
using BiodivApi.Services.PaginatorService;

namespace BiodivApi.Services.SpeciesPhotosService
{
    public interface ISpeciePhotoService
    {
        Task<SpeciePhoto> CreateSpeciePhoto(int specieId,SpeciePhotoCreateDto photoCreateDto);
        Task UpdateSpeciePhoto(SpeciePhoto speciePhoto,SpeciePhotoUpdateDto photoUpdateDto);
        Task DeleteSpeciePhoto(SpeciePhoto speciePhoto);
        Task<IEnumerable<SpecieWithPhotosReadDto>> GetPhotosWithSpecie(Paginator paginator);
        Task<SpeciePhoto> GetRandomPhoto();
        Task<SpeciePhoto> GetPhoto(int id);
    }
}