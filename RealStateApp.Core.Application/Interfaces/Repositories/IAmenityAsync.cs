using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IAmenityAsync: IGenericRepositoryAsync<Amenity>
    {
        public Task<Amenity> GetAmenityAsync(string amenityId, bool trackChanges);
    }
}
