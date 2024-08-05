using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.DataTransferObjects.PropertyImageDTOs
{
    public class PropertyImageDTO
    {
        public string Id { get; set; }
        public byte[] Image { get; set; }
        public string PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
