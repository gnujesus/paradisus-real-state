using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs
{
    public class AmenityForCreationDTO
    {
        [SwaggerParameter(Description = "El nombre de la Mejora")]
        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        /// <example>Construcción que almacena agua</example>
        [SwaggerParameter(Description = "La descripcion de la mejora")]
        [Required(ErrorMessage = "Debe colocar la descripción")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public IEnumerable<string> PropertiesIds { get; set; }
    }
}
