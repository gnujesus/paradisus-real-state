using RealStateApp.Core.Application.DataTransferObjects.Email;
using RealStateApp.Core.Domain.Settings;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {      
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
