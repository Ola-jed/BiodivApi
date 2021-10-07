using System;
using System.Linq;
using System.Threading.Tasks;
using Biodiv.Entities;
using BiodivApi.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BiodivApi.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : Entity
    {
        private readonly BiodivDbContext _dbContext;

        public GenericRepository(BiodivDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetRandom()
        {
            var query = GetAll();
            query = _dbContext.Model
                .FindEntityType(typeof(T))
                .GetNavigations()
                .Aggregate(query, (current, property) => current.Include(property.Name));
            return (await query.ToListAsync())
                .OrderBy(_ => Guid.NewGuid())
                .FirstOrDefault();
        }

        public async Task Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}