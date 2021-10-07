using System.Linq;
using System.Threading.Tasks;
using Biodiv.Entities;

namespace BiodivApi.Data.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
        Task<T> GetRandom();
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChanges();
    }
}