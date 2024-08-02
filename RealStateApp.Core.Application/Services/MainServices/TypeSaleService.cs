using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.TypeSaleModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public class TypeSaleService : GenericService<SaveTypeSaleViewModel, TypeSaleViewModel, TypeSale>, ITypeSaleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public TypeSaleService(IRepositoryManager repositoryManager, IHttpContextAccessor contextAccessor, IMapper mapper) 
            : base(repositoryManager.TypeSale, mapper)
        {
            _repositoryManager = repositoryManager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Property>> GetPropertiesByTypeSaleIdAsync(string typeSaleId)
        {
            return await _repositoryManager.TypeSale.GetPropertiesByTypeSaleIdAsync(typeSaleId);
        }

        public async Task<int> GetPropertiesCountByTypeSaleIdAsync(string typeSaleId)
        {
            return await _repositoryManager.TypeSale.GetPropertiesCountByTypeSaleIdAsync(typeSaleId);
        }
    }
}
