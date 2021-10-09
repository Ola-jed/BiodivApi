using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiodivApi.Entities;
using BiodivApi.Services.PaginatorService;

namespace BiodivApi.Data.Repositories
{
    public interface ISpecieRepository: IRepository<Specie>
    {
        Task<IEnumerable<Specie>> GetSpecies(Paginator paginator);
        Task<IEnumerable<Specie>> GetSpeciesWithPhotos(Paginator paginator);
        Task<Specie> GetSpecie(int id);
        Task<List<Specie>> Find(Expression<Func<Specie, bool>> predicate);
    }
}