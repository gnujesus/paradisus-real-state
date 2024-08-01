using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.AmenityModels
{
    public class SaveAmenityViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe colocar la descripción")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public ICollection<Property>? Properties { get; set; }
    }
}
