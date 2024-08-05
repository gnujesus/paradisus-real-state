namespace RealStateApp.Core.Application.DataTransferObjects.Account
{
    public class JwtResponse
    {
        public bool HasError { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
