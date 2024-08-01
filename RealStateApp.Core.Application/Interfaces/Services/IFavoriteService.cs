using RealStateApp.Core.Application.ViewModels.FavoritesModels;

using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IFavoriteService: IGenericService<SaveFavoritesViewModel, FavoritesViewModel, Favorites>
    {
        Task<IEnumerable<Property>> GetFavoritePropertiesByUserId(string userId);
        Task MarkFavorite(string userId, string propertyId);
        Task UnmarkFavorite(string userId, string propertyId);


    }
}
