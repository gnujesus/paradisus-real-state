using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.TypePropertyModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public class TypePropertyService: GenericService<SaveTypePropertyViewModel, TypePropertyViewModel, TypeProperty>, ITypePropertyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public TypePropertyService(IRepositoryManager repositoryManager, IHttpContextAccessor contextAccessor, IMapper mapper): base(repositoryManager.TypeProperty, mapper)
        {
            _repositoryManager = repositoryManager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }
       public async  Task<IEnumerable<Property>> GetPropertiesByTypePropertyIdAsync(string typePropertyId)
        {
            return await _repositoryManager.TypeProperty.GetPropertiesByTypePropertyIdAsync(typePropertyId);
        }
        public async Task<int> GetPropertiesCountByTypePropertyIdAsync(string typePropertyId)
        {
            return await _repositoryManager.TypeProperty.GetPropertiesCountByTypePropertyIdAsync(typePropertyId);
        }
    }
}
