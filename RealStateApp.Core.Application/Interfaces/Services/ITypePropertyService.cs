using RealStateApp.Core.Application.ViewModels.TypePropertyModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface ITypePropertyService : IGenericService<SaveTypePropertyViewModel, TypePropertyViewModel, TypeProperty>
    {
        Task<IEnumerable<Property>> GetPropertiesByTypePropertyIdAsync(string typePropertyId);
        Task<int> GetPropertiesCountByTypePropertyIdAsync(string typePropertyId);
    }
}
