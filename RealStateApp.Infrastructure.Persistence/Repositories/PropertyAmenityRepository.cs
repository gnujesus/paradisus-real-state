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
    public class PropertyAmenityRepository: GenericRepository<PropertyAmenity>, IPropertyAmenityAsync
    {
        private readonly ApplicationContext _dbContext;

        public PropertyAmenityRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
         
    }
}
