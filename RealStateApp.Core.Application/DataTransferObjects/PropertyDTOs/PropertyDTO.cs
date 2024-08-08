using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.DataTransferObjects.PropertyDTOs
{
    public class PropertyDTO
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public Agent Agent { get; set; }
        public TypeProperty TypeProperty { get; set; }
        public TypeSale TypeSale { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
    }
}
