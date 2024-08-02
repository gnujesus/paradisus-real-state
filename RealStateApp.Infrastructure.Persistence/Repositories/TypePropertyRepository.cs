using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class TypePropertyRepository: GenericRepository<TypeProperty>, ITypePropertyAsync
    {
        private readonly ApplicationContext _dbContext;

        public TypePropertyRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Property>> GetPropertiesByTypePropertyIdAsync(string typePropertyId)
        {
            return await _dbContext.Properties
                .Where(p => p.TypeProperty_Id == typePropertyId)
                .ToListAsync();
        }

        public async Task<int> GetPropertiesCountByTypePropertyIdAsync(string typePropertyId)
        {
            return await _dbContext.Properties
                .CountAsync(p => p.TypeProperty_Id == typePropertyId);
        }
    }
}
