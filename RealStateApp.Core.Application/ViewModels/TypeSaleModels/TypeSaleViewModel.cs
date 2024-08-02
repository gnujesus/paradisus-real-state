using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.ViewModels.TypeSaleModels
{
    public class TypeSaleViewModel
    {
        public virtual string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
