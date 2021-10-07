using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Data.Dtos;
using BiodivApi.Entities;

namespace BiodivApi.Services.LocalDistributionsService
{
    public interface ILocalDistributionsService
    {
        Task<LocalDistribution> GetLocalDistribution(int id);
        Task<LocalDistribution> CreateLocalDistribution(int specieId, LocalDistributionCreateDto distributionCreateDto);
        Task UpdateLocalDistribution(LocalDistribution localDistribution,
            LocalDistributionUpdateDto updateDto);
        Task DeleteLocalDistribution(LocalDistribution localDistribution);
    }
}