using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.TypePropertyModels
{
    public class SaveTypePropertyViewModel
    {
        public virtual string? Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe colocar la descripción")]
        public string Description { get; set; }

        // fk not needed
        public ICollection<Property>? Properties { get; set; }
    }
}
