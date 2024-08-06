using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.FavoritesModels;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public class FavoriteService: GenericService<SaveFavoritesViewModel, FavoritesViewModel, Favorite>, IFavoriteService
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
        //Cambiado a propertyviewmodel 
        public async Task<List<PropertyViewModel>> GetFavoritePropertiesByUserId(string userId)
        {
           var properties =   await _repositoryManager.Favorite.GetFavoritePropertiesByUserIdAsync(userId);
            var propertiesList = _mapper.Map<List<PropertyViewModel>>(properties);
            return propertiesList;
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
