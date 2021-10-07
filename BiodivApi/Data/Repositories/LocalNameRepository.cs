using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Data.Context;
using BiodivApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiodivApi.Data.Repositories
{
    public class LocalNameRepository: GenericRepository<LocalName>,ILocalNameRepository
    {
        public LocalNameRepository(BiodivDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<int>> GetSpeciesIdWithSpelling(string spelling)
        {
            return (await GetAll()
                    .Where(l => EF.Functions.Like(l.Spelling, $"%{spelling}%"))
                    .Select(l => l.SpecieId)
                    .ToListAsync())
                .ToHashSet();
        }
    }
}