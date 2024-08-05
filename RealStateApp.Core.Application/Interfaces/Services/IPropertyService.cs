using RealStateApp.Core.Application.ViewModels.PropertyModels;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<SavePropertyViewModel,PropertyViewModel,PropertyViewModel>
    {
        Task<IEnumerable<PropertyViewModel>> GetAllPropertiesWithFavorites(string userId);
        Task<PropertyViewModel> GetPropertyByCode(string code);
        Task<IEnumerable<PropertyViewModel>> GetPropertiesByAgentId(string agentId);
        Task<int> GetTotalPropertiesCount();
        Task<IEnumerable<PropertyViewModel>> FilterProperties(string typePropertyId = null!, decimal? minPrice = null, decimal? maxPrice = null, int? rooms = null, int? bathrooms = null);
        Task<List<PropertyViewModel>> GetAllProperties();
    }
}
