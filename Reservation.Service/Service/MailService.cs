using Reservation.Service.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Reservation.Service.Service
{
    internal class MailService : IMailNotification
    {
        public void SendMail(string email, string subject, string body)
        {
            //var smtpClient = new SmtpClient("smtp.gmail.com")
            //{
            //    Port = 587,
            //    Credentials = new NetworkCredential("username", "password"),
            //    EnableSsl = true
            //};

            //smtpClient.Send("reservation@issistem.com", email, subject, body);
        }
    }
}