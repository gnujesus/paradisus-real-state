using RealStateApp.Core.Application.Dtos.Email;
using RealStateApp.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {      
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
