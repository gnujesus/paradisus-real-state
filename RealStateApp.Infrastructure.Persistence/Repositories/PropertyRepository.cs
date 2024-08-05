﻿using Microsoft.EntityFrameworkCore;
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
                .Where(p => p.Favorites.Any(f => f.User_Id == userId))
                .OrderByDescending(p => p.Created)
                .ToListAsync();
        }

        public async Task<Property> GetPropertyByCodeAsync(string code)
        {
            return await context.Properties.AsNoTracking()
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == code);
        }

        public async Task<IEnumerable<Property>> GetPropertiesByAgentIdAsync(string agentId)
        {
            return await context.Properties
                .Include(p => p.Images)
                .Where(p => p.User_Id == agentId)
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
                query = query.Where(p => p.TypeProperty_Id == typePropertyId);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Value_Sale >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Value_Sale <= maxPrice.Value);
            }

            if (rooms.HasValue)
            {
                query = query.Where(p => p.Rooms == rooms.Value);
            }

            if (bathrooms.HasValue)
            {
                query = query.Where(p => p.BathRooms == bathrooms.Value);
            }

            return await query.OrderByDescending(p => p.Created).ToListAsync();
        }
    }
}
