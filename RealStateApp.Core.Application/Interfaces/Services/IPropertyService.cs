using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<SavePropertyViewModel,PropertyViewModel, PropertyViewModel>
    {
        Task<IEnumerable<Property>> GetAllPropertiesWithFavorites(string userId);
        Task<Property> GetPropertyByCode(string code);
        Task<IEnumerable<Property>> GetPropertiesByAgentId(string agentId);
        Task<int> GetTotalPropertiesCount();
        Task<IEnumerable<Property>> FilterProperties(string typePropertyId = null, decimal? minPrice = null, decimal? maxPrice = null, int? rooms = null, int? bathrooms = null);
        new Task<List<Property>> GetAll();
    }
}
