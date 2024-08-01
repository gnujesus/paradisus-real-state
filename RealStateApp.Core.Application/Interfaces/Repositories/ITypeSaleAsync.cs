using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface ITypeSaleAsync: IGenericRepositoryAsync<TypeSale>
    {
        Task<IEnumerable<Property>> GetPropertiesByTypeSaleIdAsync(string typeSaleId);
        Task<int> GetPropertiesCountByTypeSaleIdAsync(string typeSaleId);
    }
}
