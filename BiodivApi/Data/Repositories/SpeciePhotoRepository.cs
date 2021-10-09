using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiodivApi.Data.Context;
using BiodivApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiodivApi.Data.Repositories
{
    public class SpeciePhotoRepository : GenericRepository<SpeciePhoto>, ISpeciePhotoRepository
    {
        public SpeciePhotoRepository(BiodivDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<SpeciePhoto>> GetSpeciePhotos()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<List<SpeciePhoto>> Find(Expression<Func<SpeciePhoto, bool>> predicate)
        {
            return await GetAll()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<IGrouping<int, SpeciePhoto>>> GroupBySpecies()
        {
            return (await GetAll().ToListAsync())
                .GroupBy(sp => sp.SpecieId);
        }
    }
}