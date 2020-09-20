using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
//using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using ZoomIntegrationMicroservice.Models;

namespace ZoomIntegrationMicroservice.Services
{
    public class MailServiceRepo:IMailService
    {
        public bool SendEmail(EmailModel email) {
            try
            {
                string fromAddress = "Your Email Address";
                var fromPassword = "Your Password";
                var toAddress =email.To;

                var message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Your Company Name",fromAddress);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("User Name", toAddress);
                message.To.Add(to);  

                string subject = email.Subject;
                message.Subject = subject;

                BodyBuilder bodyBuilder = new BodyBuilder();
                if (email.IsHtml) {
                    bodyBuilder.HtmlBody = email.Html;
                }
                if (email.IsText) {
                    bodyBuilder.TextBody = email.Text;
                }
                if (email.IsFile) {
                    if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", email.File))) {
                        bodyBuilder.Attachments.Add(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", email.File));
                    }
                }
                message.Body = bodyBuilder.ToMessageBody();
                using (var smtpClient = new SmtpClient()) {
                    smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtpClient.Authenticate(fromAddress, fromPassword);
                    smtpClient.Send(message);
                    smtpClient.Disconnect(true);
                    smtpClient.Dispose();           
                }
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
