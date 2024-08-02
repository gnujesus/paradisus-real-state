﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        private readonly Lazy<IAmenityService> _amenityService;
        private readonly Lazy<IFavoriteService> _favoriteService;
        private readonly Lazy<IPropertyAmenityService> _propertyAmenityService;
        private readonly Lazy<IPropertyService> _propertyService;
        private readonly Lazy<IPropertyImageService> _propertyImageService;
        private readonly Lazy<ITypePropertyService> _typePropertyService;
        private readonly Lazy<ITypeSaleService> _typeSaleService;

        public ServiceManager(IRepositoryManager repositoryManager, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;

            _amenityService = new Lazy<IAmenityService>(() => new AmenityService(repositoryManager, contextAccessor, mapper));
            _favoriteService = new Lazy<IFavoriteService>(() => new FavoriteService(repositoryManager, contextAccessor, mapper));
            _propertyAmenityService = new Lazy<IPropertyAmenityService>(() => new PropertyAmenityService(repositoryManager, contextAccessor, mapper));
            _propertyService = new Lazy<IPropertyService>(() => new PropertyService(repositoryManager, contextAccessor, mapper));
            _propertyImageService = new Lazy<IPropertyImageService>(() => new PropertyImageService(repositoryManager, contextAccessor, mapper));
            _typePropertyService = new Lazy<ITypePropertyService>(() => new TypePropertyService(repositoryManager, contextAccessor, mapper));
            _typeSaleService = new Lazy<ITypeSaleService>(() => new TypeSaleService(repositoryManager, contextAccessor, mapper));
        }

        public IAmenityService Amenity => _amenityService.Value;
        public IFavoriteService Favorite => _favoriteService.Value;
        public IPropertyAmenityService PropertyAmenity => _propertyAmenityService.Value;
        public IPropertyService Property => _propertyService.Value;
        public IPropertyImageService PropertyImage => _propertyImageService.Value;
        public ITypePropertyService TypeProperty => _typePropertyService.Value;
        public ITypeSaleService TypeSale => _typeSaleService.Value;

        public async Task SaveAsync() => await _repositoryManager.SaveAsync();
    }
}