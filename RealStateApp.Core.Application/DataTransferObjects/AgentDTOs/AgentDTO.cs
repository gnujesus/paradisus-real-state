namespace RealStateApp.Core.Application.DataTransferObjects.AgentDTOs
{
    public class AgentDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PropertiesAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
