using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.DataTransferObjects.UserDTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Debe colocar el usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
