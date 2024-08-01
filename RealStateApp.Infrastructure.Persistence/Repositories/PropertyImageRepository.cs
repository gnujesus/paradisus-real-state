using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyImageRepository : GenericRepository<PropertyImage>, IPropertyImageRepository
    {
        private readonly ApplicationContext _dbContext;

        public PropertyImageRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddImagesToPropertyAsync(string propertyId, List<byte[]> images)
        {
            var property = await _dbContext.Properties
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == propertyId);

            if (property == null)
            {
                throw new Exception("Property not found");
            }

            foreach (var image in images)
            {
                property.Images.Add(new PropertyImage { Id = Guid.NewGuid().ToString(), Image = image, PropertyId = propertyId });
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveImageFromPropertyAsync(string propertyId, string imageId)
        {
            var property = await _dbContext.Properties
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == propertyId);

            if (property == null)
            {
                throw new Exception("Property not found");
            }

            var image = property.Images.FirstOrDefault(i => i.Id == imageId);
            if (image == null)
            {
                throw new Exception("Image not found");
            }

            property.Images.Remove(image);
            _dbContext.PropertyImages.Remove(image);

            await _dbContext.SaveChangesAsync();
        }
    }
}
