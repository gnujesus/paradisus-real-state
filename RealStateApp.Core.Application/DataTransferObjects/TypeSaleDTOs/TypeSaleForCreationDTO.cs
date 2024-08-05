using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs
{
    public class TypeSaleForCreationDTO
    {
        public virtual string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
