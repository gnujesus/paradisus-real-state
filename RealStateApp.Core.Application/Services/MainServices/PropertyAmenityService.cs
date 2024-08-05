using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.PropertyAmenityModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.MainServices
{

   
    public class PropertyAmenityService : GenericService<SavePropertyAmenityViewModel, PropertyAmenityViewModel, PropertyAmenity>, IPropertyAmenityService

    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public PropertyAmenityService(IRepositoryManager repositoryManager, IHttpContextAccessor contextAccessor, IMapper mapper) : base(repositoryManager.PropertyAmenity, mapper)

        {
            _repositoryManager = repositoryManager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }

    }
}
