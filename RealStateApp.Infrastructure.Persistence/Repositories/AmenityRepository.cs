using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class AmenityRepository: GenericRepository<Amenity>, IAmenityAsync
    {
        private readonly ApplicationContext _dbContext;

        public AmenityRepository(ApplicationContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
