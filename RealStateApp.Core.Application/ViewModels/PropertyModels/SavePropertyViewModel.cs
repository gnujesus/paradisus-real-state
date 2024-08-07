using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.PropertyModels
{
    public class SavePropertyViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el valor de la propiedad")]
        public decimal Value_Sale { get; set; }
        [Required(ErrorMessage = "Debe colocar el número de habitaciones de la propiedad")]
        public int Rooms { get; set; }
        [Required(ErrorMessage = "Debe colocar el número de baños de la propiedad")]
        public int BathRooms { get; set; }

        [Required(ErrorMessage = "Debe colocar el tamaño de la propiedad en metros cuadrados")]
        public string Size_Property { get; set; }

        [Required(ErrorMessage = "Debe colocar la descripción de la propiedad")]
        public string Description { get; set; }

        //Foreign Key Properties
        public string User_Id { get; set; }
        public string TypeProperty_Id { get; set; }
        public string TypeSale_Id { get; set; }

        //Navigation Properties
        public List<IFormFile> ImagesR { get; set; }
        public TypeProperty? Type_Property { get; set; }
        public TypeSale? Type_sale { get; set; }
        public ICollection<Amenity>? Amenities { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public ICollection<RealStateApp.Core.Domain.Entities.PropertyImage>? Images { get; set; } = new List<RealStateApp.Core.Domain.Entities.PropertyImage>();
    }
}
