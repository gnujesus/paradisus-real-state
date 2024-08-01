using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;

using RealStateApp.Core.Application.ViewModels.FavoritesModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public class FavoriteService: GenericService<SaveFavoritesViewModel, FavoritesViewModel, Favorites>, IFavoriteService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public FavoriteService(IRepositoryManager repositoryManager, IHttpContextAccessor contextAccessor, IMapper mapper): base(repositoryManager.Favorite, mapper)
        {
            _repositoryManager = repositoryManager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }
        //Favorite Services  , [Pending to check]
       public async Task<IEnumerable<Property>> GetFavoritePropertiesByUserId(string userId)
        {
            
          return   await _repositoryManager.Favorite.GetFavoritePropertiesByUserIdAsync(userId);
        }
        public async Task MarkFavorite(string userId, string propertyId)
        {
                await _repositoryManager.Favorite.MarkFavoriteAsync(userId, propertyId);
        }
        public async Task UnmarkFavorite(string userId, string propertyId)
        {

            await _repositoryManager.Favorite.UnmarkFavoriteAsync(userId, propertyId);

        }



    }
}
