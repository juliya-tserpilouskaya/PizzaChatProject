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
    public class Email
    {
        public static void EmailOrderComplited(string toEmailAddress, string billInfo)
        {
            SendMail(toEmailAddress, Constants.EmailOrderComplitedTitle, Constants.EmailOrderComplitedMsgBody + "\n\n" + billInfo);
        }

        public static void EmailOrderDeliveredByCourier(string toEmailAddress, string billInfo)
        {
            SendMail(toEmailAddress, Constants.EmailOrderDeliveredByCourierTitle, Constants.EmailOrderDeliveredByCourierMsgBody + "\n\n" + billInfo);
        }

        public static void EmailOrderPayment(string toEmailAddress, string billInfo)
        {
            SendMail(toEmailAddress, Constants.EmailOrderPaymentTitle, Constants.EmailOrderPaymentMsgBody + "\n\n" + billInfo);
        }

        public static  void SendMail(string toEmailAddress, string emailTitle, string emailMsgBody)
        {
            MailAddress fromAddress = new MailAddress(Constants.FromEmailAddress);
            MailAddress toAddress = new MailAddress(toEmailAddress);
            MailMessage email = new MailMessage(fromAddress, toAddress);
            email.Subject = emailTitle;
            email.Body = emailMsgBody;
            email.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587); //вынести в конст
            smtp.Credentials = new NetworkCredential(Constants.FromEmailAddress, Constants.FromEmailPassword);
            smtp.EnableSsl = true;
            smtp.Send(email);
        }
    }
}
