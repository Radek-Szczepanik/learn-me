using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearnMe.Core.Services.Account.Email;

namespace LearnMe.Core.Interfaces.Services
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
