using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.PropertyImage;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public class PropertyImageService : GenericService<SavePropertyImageViewModel, PropertyImageViewModel, PropertyImage>, IPropertyImageService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public PropertyImageService(IPropertyImageRepository propertyImageRepository, 
            IHttpContextAccessor contextAccessor, 
            IMapper mapper) : base(propertyImageRepository, mapper)
        {
            _propertyImageRepository = propertyImageRepository;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }

        public async Task AddImagesToPropertyAsync(string propertyId, List<byte[]> images)
        {
            await _propertyImageRepository.AddImagesToPropertyAsync(propertyId, images);
        }

        public async Task RemoveImageFromPropertyAsync(string propertyId, string imageId)
        {
            await _propertyImageRepository.RemoveImageFromPropertyAsync(propertyId, imageId);
        }
    }
}
