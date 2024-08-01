using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IFavoriteAsync: IGenericRepositoryAsync<Favorites>
    {
        Task<IEnumerable<Property>> GetFavoritePropertiesByUserIdAsync(string userId);
        Task MarkFavoriteAsync(string userId, string propertyId);
        Task UnmarkFavoriteAsync(string userId, string propertyId);
    }
}
