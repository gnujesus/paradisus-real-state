﻿using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class FavoritesRepository: GenericRepository<Favorites>, IFavoriteAsync
    {
        private readonly ApplicationContext context;

        public FavoritesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<IEnumerable<Property>> GetFavoritePropertiesByUserIdAsync(string userId)
        {
            return await context.Favorites
                .Where(f => f.User_Id == userId)
                .Include(f => f.Property)
                .Select(f => f.Property)
                .ToListAsync();
        }

        public async Task MarkFavoriteAsync(string userId, string propertyId)
        {
            var favorite = new Favorites { User_Id = userId, Property_Id = propertyId };
            context.Favorites.Add(favorite);
            await context.SaveChangesAsync();
        }

        public async Task UnmarkFavoriteAsync(string userId, string propertyId)
        {
            var favorite = await context.Favorites.FirstOrDefaultAsync(f => f.User_Id == userId && f.Property_Id == propertyId);
            if (favorite != null)
            {
                context.Favorites.Remove(favorite);
                await context.SaveChangesAsync();
            }
        }
    }
}
