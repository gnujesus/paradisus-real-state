namespace RealStateApp.Core.Application.DataTransferObjects.Email
{
    public class EmailRequest
    {
        public string To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string From { get; set; }
    }
}
