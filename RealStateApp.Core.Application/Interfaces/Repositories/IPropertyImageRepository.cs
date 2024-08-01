using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IPropertyImageRepository : IGenericRepositoryAsync<PropertyImage>
    {
        Task AddImagesToPropertyAsync(string propertyId, List<byte[]> images);
        Task RemoveImageFromPropertyAsync(string propertyId, string imageId);
    }
}
