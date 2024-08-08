using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class TypeSaleRepository: GenericRepository<TypeSale>, ITypeSaleAsync
    {
        private readonly ApplicationContext context;

        public TypeSaleRepository(ApplicationContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<IEnumerable<Property>> GetPropertiesByTypeSaleIdAsync(string typeSaleId)
        {
            return await context.Properties
                .Where(p => p.PropertyTypeSaleId == typeSaleId)
                .ToListAsync();
        }

        public async Task<int> GetPropertiesCountByTypeSaleIdAsync(string typeSaleId)
        {
            return await context.Properties
                .CountAsync(p => p.PropertyTypeSaleId == typeSaleId);
        }
    }
}
