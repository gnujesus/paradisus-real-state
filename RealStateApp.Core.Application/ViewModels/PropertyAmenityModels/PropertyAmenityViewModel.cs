using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.PropertyAmenityModels
{
    public class PropertyAmenityViewModel
    {
        public string PropertyId { get; set; }
        public Property Property { get; set; }

        public string AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}
