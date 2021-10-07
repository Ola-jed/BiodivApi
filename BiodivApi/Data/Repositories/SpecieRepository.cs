using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Data.Context;
using BiodivApi.Entities;
using BiodivApi.Services.PaginatorService;
using Microsoft.EntityFrameworkCore;

namespace BiodivApi.Data.Repositories
{
    public class SpecieRepository : GenericRepository<Specie>, ISpecieRepository
    {
        public SpecieRepository(BiodivDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Specie>> GetSpecies(Paginator paginator)
        {
            return await GetAll()
                .OrderBy(s => s.Id)
                .Skip(paginator.Offset)
                .Take(paginator.PageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Specie>> GetSpeciesWithPhotos(Paginator paginator)
        {
            return await GetAll()
                .OrderBy(s => s.Id)
                .Skip(paginator.Offset)
                .Take(paginator.PageSize)
                .Include(s => s.SpeciePhotos)
                .ToListAsync();
        }

        public async Task<Specie> GetSpecie(int id)
        {
            return await GetAll()
                .Include(s => s.LocalDistributions)
                .Include(s => s.LocalNames)
                .Include(s => s.SpeciePhotos)
                .AsSplitQuery()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Specie>> Find(Expression<Func<Specie, bool>> predicate)
        {
            return await GetAll()
                .Where(predicate)
                .ToListAsync();
        }
    }
}