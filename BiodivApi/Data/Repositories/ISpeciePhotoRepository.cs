using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiodivApi.Entities;

namespace BiodivApi.Data.Repositories
{
    public interface ISpeciePhotoRepository : IRepository<SpeciePhoto>
    {
        Task<IEnumerable<SpeciePhoto>> GetSpeciePhotos();
        Task<List<SpeciePhoto>> Find(Expression<Func<SpeciePhoto, bool>> predicate);
        Task<IEnumerable<IGrouping<int, SpeciePhoto>>> GroupBySpecies();
    }
}