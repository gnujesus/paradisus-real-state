namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyAmenity
    {
        public string PropertyId { get; set; }
        public Property Property { get; set; }

        public string AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}
