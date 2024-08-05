using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public class PropertyService : GenericService<SavePropertyViewModel, PropertyViewModel, Property>, IPropertyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        // Changed a propertyviewmodel
        public PropertyService(IRepositoryManager repositoryManager, IHttpContextAccessor contextAccessor, IMapper mapper): base(repositoryManager.Property, mapper)
        {
            _repositoryManager = repositoryManager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PropertyViewModel>> GetAllPropertiesWithFavorites(string userId)
        {
            var Properties = await _repositoryManager.Property.GetAllPropertiesWithFavoritesAsync(userId);
            return _mapper.Map<List<PropertyViewModel>>(Properties);
        }
        public async Task<PropertyViewModel> GetPropertyByCode(string code)
        {
            var Properties = await _repositoryManager.Property.GetPropertyByCodeAsync(code);
            return _mapper.Map<PropertyViewModel>(Properties);
        }

        public async Task<IEnumerable<PropertyViewModel>> GetPropertiesByAgentId(string agentId)
        {
            await Task.Run(() => { });
            var Properties = _repositoryManager.Property.GetPropertiesByAgentIdAsync(agentId);

            return _mapper.Map<List<PropertyViewModel>>(Properties);
        }

        public async Task<int> GetTotalPropertiesCount()
        {
            return await _repositoryManager.Property.GetTotalPropertiesCountAsync();
        }
        public async Task<IEnumerable<PropertyViewModel>> FilterProperties(string typePropertyId, decimal? minPrice, decimal? maxPrice, int? rooms, int? bathrooms)
        {
            var Properties = await _repositoryManager.Property.FilterPropertiesAsync(typePropertyId, minPrice, maxPrice, rooms, bathrooms);
            return _mapper.Map<List<PropertyViewModel>>(Properties);
        }

        //public async Task<IEnumerable<PropertyViewModel>> FilterProperties(string typePropertyId = "", decimal? minPrice = null, decimal? maxPrice = null, int? rooms = null, int? bathrooms = null)
        //{
        //    var Properties = await _repositoryManager.Property.FilterPropertiesAsync(typePropertyId = "", minPrice = null, maxPrice = null, rooms = null, bathrooms);
        //    return   _mapper.Map<List<PropertyViewModel>>(Properties);
        //}
        public async Task<List<PropertyViewModel>> GetAllProperties()
        {
            var Properties = await _repositoryManager.Property.GetAllAsync();
            return _mapper.Map<List<PropertyViewModel>>(Properties);
        }
    }
}
