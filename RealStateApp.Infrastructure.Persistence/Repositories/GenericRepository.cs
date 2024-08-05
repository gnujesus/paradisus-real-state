using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Infrastructure.Persistence.Contexts;
using RealStateApp.Infrastructure.Persistence.Services;
using System.Linq.Expressions;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepositoryAsync<Entity> where Entity : class
    {
        private readonly ApplicationContext context;

        public GenericRepository(ApplicationContext dbContext)
        {
            context = dbContext;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            var idProperty = entity.GetType().GetProperty("Id");
            if (idProperty != null && idProperty.PropertyType == typeof(string))
            {
                var currentId = idProperty.GetValue(entity) as string;
                if (string.IsNullOrEmpty(currentId))
                {
                    var generatedId = await CodeGenerator.GenerateUniqueCodeAsync(context);
                    idProperty.SetValue(entity, generatedId);
                }
            }

            await context.Set<Entity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(Entity entity, string id)
        {
            var entry = await context.Set<Entity>().FindAsync(id);
            if (entry != null)
            {
                context.Entry(entry).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
            }
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            context.Set<Entity>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsync(bool trackChanges = false)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(string id, bool trackChanges = false)
        {
            return (await FindByCondition(e => EF.Property<string>(e, "Id") == id, trackChanges).FirstOrDefaultAsync())!;
        }

        public virtual async Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties, bool trackChanges = false)
        {
            var query = FindAll(trackChanges);

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        private IQueryable<Entity> FindAll(bool trackChanges) =>
            !trackChanges ?
              context.Set<Entity>()
                .AsNoTracking() :
              context.Set<Entity>();

        private IQueryable<Entity> FindByCondition(Expression<Func<Entity, bool>> expression,
            bool trackChanges) =>
                !trackChanges ?
                  context.Set<Entity>()
                    .Where(expression)
                    .AsNoTracking() :
                  context.Set<Entity>()
                    .Where(expression);
    }
}
