using RealStateApp.Core.Application.ViewModels.AmenityModels;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IAmenityService: IGenericService<SaveAmenityViewModel, AmenityViewModel, Amenity>
    {
    }
}
