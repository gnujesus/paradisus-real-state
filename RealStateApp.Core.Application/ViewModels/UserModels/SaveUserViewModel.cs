using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.ViewModels.UserModels
{
    public class SaveUserViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Debe colocar un apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar un número de telefono")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
        public byte[]? Image { get; set; }

        [Required(ErrorMessage = "Debe colocar la foto")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "Debe colocar la Email")]
        [DataType(DataType.Upload)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Upload)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseñá")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas deben coincidir")]
        [Required(ErrorMessage = "Debe colocar una contraseñá")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Type_User { get; set; }
        public string? EmailVerified { get; set; }
        public bool IsActive { get; set; }
        public bool? HasError { get; set; }
        public string? Error { get; set; }


        //Navigation Property
    }
}
