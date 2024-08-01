using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class FavoritesRepository: GenericRepository<Favorites>, IFavoriteAsync
    {
        private readonly ApplicationContext _dbContext;

        public FavoritesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Property>> GetFavoritePropertiesByUserIdAsync(string userId)
        {
            return await _dbContext.Favorites
                .Where(f => f.User_Id == userId)
                .Include(f => f.Property)
                .Select(f => f.Property)
                .ToListAsync();
        }

        public async Task MarkFavoriteAsync(string userId, string propertyId)
        {
            var favorite = new Favorites { User_Id = userId, Property_Id = propertyId };
            _dbContext.Favorites.Add(favorite);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UnmarkFavoriteAsync(string userId, string propertyId)
        {
            var favorite = await _dbContext.Favorites.FirstOrDefaultAsync(f => f.User_Id == userId && f.Property_Id == propertyId);
            if (favorite != null)
            {
                _dbContext.Favorites.Remove(favorite);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
