using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.DataTransferObjects.PropertyAmenityDTOs
{
    public class PropertyDTO
    {
        public string PropertyId { get; set; }
        public Property Property { get; set; }

        public string AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}
