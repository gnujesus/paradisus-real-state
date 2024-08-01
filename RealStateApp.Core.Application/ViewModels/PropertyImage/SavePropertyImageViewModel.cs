using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.PropertyImage
{
    public class SavePropertyImageViewModel
    {
        public string Id { get; set; }
        public byte[]? Image { get; set; }
        public string PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
