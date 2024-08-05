namespace RealStateApp.Core.Application.DataTransferObjects.UserDTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public byte[] Image { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type_user { get; set; }
        public string EmailVerified { get; set; }
    }

}