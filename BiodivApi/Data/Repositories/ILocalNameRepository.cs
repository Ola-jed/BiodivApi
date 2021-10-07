using System.Collections.Generic;
using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Entities;

namespace BiodivApi.Data.Repositories
{
    public interface ILocalNameRepository: IRepository<LocalName>
    {
        Task<IEnumerable<int>> GetSpeciesIdWithSpelling(string spelling);
    }
}