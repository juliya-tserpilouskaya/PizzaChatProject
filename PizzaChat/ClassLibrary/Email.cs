using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Mail;

namespace ClassLibrary
{
    class Email
    {
        public void EmailOrderComplited(string toEmailAddress)
        {
            SendMail(toEmailAddress, Constants.EmailOrderComplitedTitle, Constants.EmailOrderComplitedMsgBody);
        }

        public void EmailOrderDeliveredByCourier(string toEmailAddress)
        {
            SendMail(toEmailAddress, Constants.EmailOrderDeliveredByCourierTitle, Constants.EmailOrderDeliveredByCourierMsgBody);
        }

        public void EmailOrderPayment(string toEmailAddress)
        {
            SendMail(toEmailAddress, Constants.EmailOrderPaymentTitle, Constants.EmailOrderPaymentMsgBody);
        }

        public void SendMail(string toEmailAddress, string emailTitle, string emailMsgBody)
        {
            MailAddress fromAddress = new MailAddress(Constants.FromEmailAddress);
            MailAddress toAddress = new MailAddress(toEmailAddress);
            MailMessage email = new MailMessage(fromAddress, toAddress);
            email.Subject = emailTitle;
            email.Body = emailMsgBody;
            email.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential(Constants.FromEmailAddress, Constants.FromEmailPassword);
            smtp.EnableSsl = true;
            smtp.Send(email);
        }
    }
}
