using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs
{
    public class AmenityWithoutPropertiesDTO
    {
        [SwaggerParameter(Description = "El id de la Mejora que se desea seleccionar")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PropertiesQuantity { get; set; }
    }
}
