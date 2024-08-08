using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IPropertyAsync: IGenericRepositoryAsync<Property>
    {
        Task<IEnumerable<Property>> GetAllPropertiesWithFavoritesAsync(string userId);
        Task<Property> GetPropertyByCodeAsync(string code);
        Task<IEnumerable<Property>> GetPropertiesByAgentIdAsync(string agentId);
        Task<int> GetTotalPropertiesCountAsync();
        Task<IEnumerable<Property>> FilterPropertiesAsync(string typePropertyId = null!, decimal? minPrice = null, decimal? maxPrice = null, int? rooms = null, int? bathrooms = null);
        Task<Property> GetByCodeWithIncludeAsync(string code, List<string> properties, bool trackChanges = false);
    }
}
