﻿using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class TypeSale: AuditableBaseEntity
    {
        public string Name { get; set; } 
        public string Description { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
