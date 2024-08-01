using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class TypeSaleRepository: GenericRepository<TypeSale>, ITypeSaleAsync
    {
        private readonly ApplicationContext _dbContext;

        public TypeSaleRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Core.Domain.Entities.Property>> GetPropertiesByTypeSaleIdAsync(string typeSaleId)
        {
            return await _dbContext.Properties
                .Where(p => p.TypeSale_Id == typeSaleId)
                .ToListAsync();
        }

        public async Task<int> GetPropertiesCountByTypeSaleIdAsync(string typeSaleId)
        {
            return await _dbContext.Properties
                .CountAsync(p => p.TypeSale_Id == typeSaleId);
        }
    }
}
