using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public class PropertyService: GenericService<SavePropertyViewModel, PropertyViewModel, Property>, IPropertyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public PropertyService(IRepositoryManager repositoryManager, IHttpContextAccessor contextAccessor, IMapper mapper): base(repositoryManager.Property, mapper)
        {
            _repositoryManager = repositoryManager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Property>> GetAllPropertiesWithFavorites(string userId)
        {


            return await _repositoryManager.Property.GetAllPropertiesWithFavoritesAsync(userId);
        }
        public async Task<Property> GetPropertyByCode(string code)
        {
           return await _repositoryManager.Property.GetPropertyByCodeAsync(code);
        }

        public async Task<IEnumerable<Property>> GetPropertiesByAgentId(string agentId)
        {
            return await _repositoryManager.Property.GetPropertiesByAgentIdAsync(agentId);
        }

        public async Task<int> GetTotalPropertiesCount()
        {
            return await _repositoryManager.Property.GetTotalPropertiesCountAsync();
        }
        public async Task<IEnumerable<Property>> FilterProperties(string typePropertyId = "", decimal? minPrice = null, decimal? maxPrice = null, int? rooms = null, int? bathrooms = null)
        {
           return  await _repositoryManager.Property.FilterPropertiesAsync( typePropertyId = "",  minPrice = null, maxPrice = null,  rooms = null, bathrooms);
        }
        public async Task<List<Property>> GetAll()
        {
            return await _repositoryManager.Property.GetAllAsync();
        }
    }
}
