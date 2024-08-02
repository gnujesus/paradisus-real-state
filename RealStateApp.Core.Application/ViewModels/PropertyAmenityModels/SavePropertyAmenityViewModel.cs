using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.PropertyAmenityModels
{
    public class SavePropertyAmenityViewModel
    {
        [Required(ErrorMessage = "Debe colocar el id")]
        public string PropertyId { get; set; }
        public Property Property { get; set; }

        [Required(ErrorMessage = "Debe colocar el id")]
        public string AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}
