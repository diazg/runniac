using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Email
{
    public interface IEmailSender
    {
        void SendMessage(string from, string to, string subject, string body);
    }
}
