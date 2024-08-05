using RealStateApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs
{
    public class TypeSaleDTO
    {
        public virtual string Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe colocar la descripción")]
        public string Description { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
