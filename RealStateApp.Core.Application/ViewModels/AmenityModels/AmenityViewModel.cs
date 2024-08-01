using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.ViewModels.AmenityModels
{
    public class AmenityViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
