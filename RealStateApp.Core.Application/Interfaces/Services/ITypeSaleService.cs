using RealStateApp.Core.Application.ViewModels.TypeSaleModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface ITypeSaleService : IGenericService<SaveTypeSaleViewModel, TypeSaleViewModel, TypeSale>
    {
        Task<IEnumerable<Property>> GetPropertiesByTypeSaleIdAsync(string typeSaleId);
        Task<int> GetPropertiesCountByTypeSaleIdAsync(string typeSaleId);
    }
}
