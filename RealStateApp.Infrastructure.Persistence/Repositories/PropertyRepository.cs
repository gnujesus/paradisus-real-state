using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository: GenericRepository<Property>, IPropertyAsync
    {
        private readonly ApplicationContext context;

        public PropertyRepository(ApplicationContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public override async Task<List<Property>> GetAllAsync(bool trackChanges = false)
        {
            return await context.Properties
                .Include(p => p.Images)
                .OrderByDescending(p => p.Created)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesWithFavoritesAsync(string userId)
        {
            return await context.Properties
                .Include(p => p.Favorites)
                .Include(p => p.Images)
                .Where(p => p.Favorites.Any(f => f.UserId == userId))
                .OrderByDescending(p => p.Created)
                .ToListAsync();
        }

        public async Task<Property> GetPropertyByCodeAsync(string code)
        {
            return await context.Properties.AsNoTracking()
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == code);
        }

        public async Task<Property> GetByCodeWithIncludeAsync(string code, List<string> properties, bool trackChanges = false)
        {
            var query = trackChanges ? context.Set<Property>() : context.Set<Property>().AsNoTracking();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.SingleOrDefaultAsync(e => EF.Property<string>(e, "Code") == code);
        }

        public async Task<IEnumerable<Property>> GetPropertiesByAgentIdAsync(string agentId)
        {
            return await context.Properties
                .Include(p => p.Images)
                .Where(p => p.UserId == agentId)
                .OrderByDescending(p => p.Created)
                .ToListAsync();
        }

        public async Task<int> GetTotalPropertiesCountAsync()
        {
            return await context.Properties.CountAsync();
        }

        public async Task<IEnumerable<Property>> FilterPropertiesAsync(string typePropertyId = null!, decimal? minPrice = null, decimal? maxPrice = null, int? rooms = null, int? bathrooms = null)
        {
            var query = context.Properties
                .Include(p => p.Images)
                .AsQueryable();

            if (!string.IsNullOrEmpty(typePropertyId))
            {
                query = query.Where(p => p.TypePropertyId == typePropertyId);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if (rooms.HasValue)
            {
                query = query.Where(p => p.Rooms == rooms.Value);
            }

            if (bathrooms.HasValue)
            {
                query = query.Where(p => p.Bathrooms == bathrooms.Value);
            }

            return await query.OrderByDescending(p => p.Created).ToListAsync();
        }
    }
}
