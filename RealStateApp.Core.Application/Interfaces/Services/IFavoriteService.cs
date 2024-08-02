using RealStateApp.Core.Application.ViewModels.FavoritesModels;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IFavoriteService: IGenericService<SaveFavoritesViewModel, FavoritesViewModel, Favorites>
    {
        Task<IEnumerable<PropertyViewModel>> GetFavoritePropertiesByUserId(string userId);
        Task MarkFavorite(string userId, string propertyId);
        Task UnmarkFavorite(string userId, string propertyId);


    }
}
