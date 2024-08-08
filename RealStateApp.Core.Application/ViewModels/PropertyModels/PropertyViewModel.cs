using RealStateApp.Core.Domain.Entities;
using RealStateApp.Core.Application.ViewModels.UserModels;

namespace RealStateApp.Core.Application.ViewModels.PropertyModels
{
    public class PropertyViewModel
    {
        public string? Id { get; set; }
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
        public Agent Agent { get; set; }
        public TypeProperty TypeProperty { get; set; }
        public TypeSale TypeSale { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<RealStateApp.Core.Domain.Entities.PropertyImage> Images { get; set; } = new List<RealStateApp.Core.Domain.Entities.PropertyImage>();
    }
}
