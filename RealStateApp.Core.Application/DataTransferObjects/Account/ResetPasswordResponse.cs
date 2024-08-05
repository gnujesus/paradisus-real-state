namespace RealStateApp.Core.Application.DataTransferObjects.Account
{
    public class ResetPasswordResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
