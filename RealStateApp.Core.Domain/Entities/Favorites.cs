﻿using RealStateApp.Core.Domain.Common;

namespace RealStateApp.Core.Domain.Entities
{
    public class Favorites: AuditableBaseEntity
    {
        public string User_Id { get; set; }
        public string Property_Id { get; set; }
        public Property Property { get; set; }

    }
}
