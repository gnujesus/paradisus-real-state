using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.ViewModels.PropertyModels
{
    public class PropertyViewModel
    {
        public string? Id { get; set; }
        public decimal Value_Sale { get; set; }
        public int Rooms { get; set; }
        public int BathRooms { get; set; }
        public string Size_Property { get; set; }
        public string Description { get; set; }

        //Foreign Key Properties
        public string User_Id { get; set; }
        public string TypeProperty_Id { get; set; }
        public string TypeSale_Id { get; set; }

        //Navigation Properties
        public TypeProperty Type_Property { get; set; }
        public TypeSale Type_sale { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public ICollection<Favorites> Favorites { get; set; }
        public ICollection<Domain.Entities.PropertyImage> Images { get; set; } = new List<Domain.Entities.PropertyImage>();
    }
}
