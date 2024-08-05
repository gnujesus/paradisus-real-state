using System.ComponentModel.DataAnnotations;

namespace RealStateApp.Core.Application.DataTransferObjects.UserDTOs
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "Debe colocar el correo del usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
