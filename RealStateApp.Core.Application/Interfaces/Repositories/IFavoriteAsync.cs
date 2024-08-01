using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IFavoriteAsync: IGenericRepositoryAsync<Favorites>
    {
        Task<IEnumerable<Property>> GetFavoritePropertiesByUserIdAsync(string userId);
        Task MarkFavoriteAsync(string userId, string propertyId);
        Task UnmarkFavoriteAsync(string userId, string propertyId);
    }
}
