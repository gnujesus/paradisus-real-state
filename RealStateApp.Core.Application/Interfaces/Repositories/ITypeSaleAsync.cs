using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface ITypeSaleAsync: IGenericRepositoryAsync<TypeSale>
    {
        Task<IEnumerable<Property>> GetPropertiesByTypeSaleIdAsync(string typeSaleId);
        Task<int> GetPropertiesCountByTypeSaleIdAsync(string typeSaleId);
    }
}
