﻿using RealStateApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.FavoritesModels
{
    public class SaveFavoritesViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el Id del usuario")]
        [DataType(DataType.Text)]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Debe colocar el Id de la propiedad")]
        [DataType(DataType.Text)]
        public string? Property_Id { get; set; }
        public Property Property { get; set; }
    }
}
