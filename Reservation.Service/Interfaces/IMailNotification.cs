using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Service.Interfaces
{
    internal interface IMailNotification
    {
        void SendMail(string email, string subject, string body);
    }
}
