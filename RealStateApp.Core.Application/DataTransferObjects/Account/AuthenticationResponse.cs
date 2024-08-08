using System.Text.Json.Serialization;

namespace RealStateApp.Core.Application.DataTransferObjects.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new();
        public bool IsActive { get; set; }
        public string NationalId { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public string JWToken { get; set; } = string.Empty;
        [JsonIgnore]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
