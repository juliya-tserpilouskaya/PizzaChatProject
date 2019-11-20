using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Mail;
using Logger;
using System.Threading;

namespace ClassLibrary
{
    public class Email
    {
        public static void EmailOrderComplited(string toEmailAddress, string billInfo, CustomLogger logger)
        {
            SendMail(toEmailAddress, Constants.EmailOrderComplitedTitle, Constants.EmailOrderComplitedMsgBody + "\n\n" + billInfo, logger);
        }

        public static void EmailOrderDeliveredByCourier(string toEmailAddress, string billInfo, CustomLogger logger)
        {
            SendMail(toEmailAddress, Constants.EmailOrderDeliveredByCourierTitle, Constants.EmailOrderDeliveredByCourierMsgBody + "\n\n" + billInfo, logger);
        }

        public static void EmailOrderPayment(string toEmailAddress, string billInfo, CustomLogger logger)
        {
            SendMail(toEmailAddress, Constants.EmailOrderPaymentTitle, Constants.EmailOrderPaymentMsgBody + "\n\n" + billInfo, logger);
        }

        public static void SendMail(string toEmailAddress, string emailTitle, string emailMsgBody, CustomLogger logger)
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
            try
            {
                smtp.Send(email);
                logger.UseLogger("INFO", "Письмо успешно доставлено", Thread.GetDomainID().ToString(), "SendMail");
            }
            catch (SmtpFailedRecipientsException ex)
            {
                for (int i = 0; i < ex.InnerExceptions.Length; i++)
                {
                    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                        status == SmtpStatusCode.MailboxUnavailable)
                    {
                        logger.UseLogger("ERROR", "Delivery failed - retrying in 5 seconds.", Thread.GetDomainID().ToString(), "SendMail");
                        System.Threading.Thread.Sleep(5000);
                        smtp.Send(email);
                    }
                    else
                    {
                        logger.UseLogger("ERROR", "Failed to deliver message to " + ex.InnerExceptions[i].FailedRecipient, Thread.GetDomainID().ToString(), "SendMail");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.UseLogger("ERROR", "Exception caught in RetryIfBusy(): " + ex.ToString(), Thread.GetDomainID().ToString(), "SendMail");
            }
        }
    }
}
