using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class Agent : AuditableBaseEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PropertiesAmount { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Property> Properties { get; set; }
    }
}
