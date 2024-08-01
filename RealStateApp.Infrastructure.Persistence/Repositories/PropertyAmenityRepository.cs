using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

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
