using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface ITypePropertyAsync: IGenericRepositoryAsync<TypeProperty>
    {
        Task<IEnumerable<Core.Domain.Entities.Property>> GetPropertiesByTypePropertyIdAsync(string typePropertyId);
        Task<int> GetPropertiesCountByTypePropertyIdAsync(string typePropertyId);
    }
}
