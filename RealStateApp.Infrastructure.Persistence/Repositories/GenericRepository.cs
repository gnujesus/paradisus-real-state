using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Infrastructure.Persistence.Contexts;
using System.Linq.Expressions;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepositoryAsync<Entity> where Entity : class
    {
        private readonly ApplicationContext _dbContext;

        public GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(Entity entity, string id)
        {
            var entry = await _dbContext.Set<Entity>().FindAsync(id);
            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _dbContext.Set<Entity>().ToListAsync();//Deferred execution
        }

        public virtual async Task<Entity> GetByIdAsync(string id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }

        public IQueryable<Entity> FindAll(bool trackChanges) =>
            !trackChanges ?
              _dbContext.Set<Entity>()
                .AsNoTracking() :
              _dbContext.Set<Entity>();

        public IQueryable<Entity> FindByCondition(Expression<Func<Entity, bool>> expression,
            bool trackChanges) =>
                !trackChanges ?
                  _dbContext.Set<Entity>()
                    .Where(expression)
                    .AsNoTracking() :
                  _dbContext.Set<Entity>()
                    .Where(expression);

        public virtual async Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties)
        {
            var query = _dbContext.Set<Entity>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }
    }
}
