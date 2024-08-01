using RealStateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class Favorites: AuditableBaseEntity
    {
        public string User_Id { get; set; }
        public string Property_Id { get; set; }
        public Property Property { get; set; }

    }
}
