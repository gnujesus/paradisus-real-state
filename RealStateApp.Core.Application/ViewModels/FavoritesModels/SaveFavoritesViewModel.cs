using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.FavoritesModels
{
    public class SaveFavoritesViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el Id del usuario")]
        [DataType(DataType.Text)]
        public string? User_Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el Id de la propiedad")]
        [DataType(DataType.Text)]
        public string? Property_Id { get; set; }
        public Property Property { get; set; }
    }
}
