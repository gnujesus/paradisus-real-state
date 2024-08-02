using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Application.ViewModels.TypeSaleModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface ITypeSaleService : IGenericService<SaveTypeSaleViewModel, TypeSaleViewModel, TypeSale>
    {
        Task<IEnumerable<PropertyViewModel>> GetPropertiesByTypeSaleIdAsync(string typeSaleId);
        Task<int> GetPropertiesCountByTypeSaleIdAsync(string typeSaleId);
    }
}
