using RealStateApp.Core.Application.ViewModels.PropertyImage;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyImageService : IGenericService<SavePropertyImageViewModel, PropertyImageViewModel, PropertyImage>
    {
        Task AddImagesToPropertyAsync(string propertyId, List<byte[]> images);
        Task RemoveImageFromPropertyAsync(string propertyId, string imageId);
    }
}
