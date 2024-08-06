using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class FavoritesRepository: GenericRepository<Favorite>, IFavoriteAsync
    {
        private readonly ApplicationContext context;

        public FavoritesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<IEnumerable<Property>> GetFavoritePropertiesByUserIdAsync(string userId)
        {
            return await context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Property)
                .Select(f => f.Property)
                .ToListAsync();
        }

        public async Task MarkFavoriteAsync(string userId, string propertyId)
        {
            var favorite = new Favorite { UserId = userId, Property_Id = propertyId };
            context.Favorites.Add(favorite);
            await context.SaveChangesAsync();
        }

        public async Task UnmarkFavoriteAsync(string userId, string propertyId)
        {
            var favorite = await context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.Property_Id == propertyId);
            if (favorite != null)
            {
                context.Favorites.Remove(favorite);
                await context.SaveChangesAsync();
            }
        }
    }
}
