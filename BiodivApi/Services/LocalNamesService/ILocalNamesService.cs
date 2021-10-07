using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;

namespace BiodivApi.Services.LocalNamesService
{
    public interface ILocalNamesService
    {
        Task<LocalName> GetLocalName(int id);
        Task<LocalName> CreateLocalName(int specieId, LocalNameCreateDto localNameCreateDto);
        Task UpdateLocalName(LocalName localName,LocalNameUpdateDto localNameUpdateDto);
        Task DeleteLocalName(LocalName localName);
    }
}