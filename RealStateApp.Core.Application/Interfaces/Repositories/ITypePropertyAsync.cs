using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface ITypePropertyAsync: IGenericRepositoryAsync<TypeProperty>
    {
        Task<IEnumerable<Property>> GetPropertiesByTypePropertyIdAsync(string typePropertyId);
        Task<int> GetPropertiesCountByTypePropertyIdAsync(string typePropertyId);
    }
}
