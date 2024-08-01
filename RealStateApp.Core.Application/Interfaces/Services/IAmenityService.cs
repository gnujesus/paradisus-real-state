using RealStateApp.Core.Application.ViewModels.AmenityModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IAmenityService: IGenericService<SaveAmenityViewModel, AmenityViewModel, Amenity>
    {
    }
}
