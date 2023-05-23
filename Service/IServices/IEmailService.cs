using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IEmailService
    {
        void SendEmail(string recipient, string subject, string body);

        void SendSmtpEmail(string recipient, string subject, string body);
    }
}
