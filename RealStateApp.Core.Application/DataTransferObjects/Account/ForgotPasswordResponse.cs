namespace RealStateApp.Core.Application.DataTransferObjects.Account
{
    public class ForgotPasswordResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
