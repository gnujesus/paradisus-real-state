using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.DataTransferObjects.TypePropertyDTOs
{
    public class TypePropertyForCreationDTO
    {
        public virtual string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
