using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Application.ViewModels.TypePropertyModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface ITypePropertyService : IGenericService<SaveTypePropertyViewModel, TypePropertyViewModel, TypeProperty>
    {
        Task<IEnumerable<PropertyViewModel>> GetPropertiesByTypePropertyIdAsync(string typePropertyId);
        Task<int> GetPropertiesCountByTypePropertyIdAsync(string typePropertyId);
    }
}
