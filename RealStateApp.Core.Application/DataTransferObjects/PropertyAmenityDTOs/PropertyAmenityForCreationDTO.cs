using RealStateApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.DataTransferObjects.PropertyAmenityDTOs
{
    public class PropertyAmenityForCreationDTO
    {
        [Required(ErrorMessage = "Debe colocar el id")]
        public string PropertyId { get; set; }
        public Property Property { get; set; }

        [Required(ErrorMessage = "Debe colocar el id")]
        public string AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}
