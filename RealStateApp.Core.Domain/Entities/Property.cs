using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class Property: AuditableBaseEntity
    {
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }

        //Foreign Key Properties
        public string UserId { get; set; }
        public string TypePropertyId { get; set; }
        public string PropertyTypeSaleId { get; set; }

        //Navigation Properties
        public TypeProperty TypeProperty { get; set; }
        public TypeSale TypeSale { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
    }
}
