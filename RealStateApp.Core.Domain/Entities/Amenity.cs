﻿using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class Amenity: AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
