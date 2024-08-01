using RealStateApp.Core.Application.ViewModels.PropertyAmenityModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyAmenityService : IGenericService<SavePropertyAmenityViewModel, PropertyAmenityViewModel, PropertyAmenity>  
    {
    }
}
