using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class TypePropertyRepository: GenericRepository<TypeProperty>, ITypePropertyAsync
    {
        private readonly ApplicationContext context;

        public TypePropertyRepository(ApplicationContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<IEnumerable<Property>> GetPropertiesByTypePropertyIdAsync(string typePropertyId)
        {
            return await context.Properties
                .Where(p => p.TypePropertyId == typePropertyId)
                .ToListAsync();
        }

        public async Task<int> GetPropertiesCountByTypePropertyIdAsync(string typePropertyId)
        {
            return await context.Properties
                .CountAsync(p => p.TypePropertyId == typePropertyId);
        }
    }
}
