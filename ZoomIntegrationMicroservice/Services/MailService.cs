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
using Microsoft.Extensions.Configuration;
using MimeKit;
using ZoomIntegrationMicroservice.Models;

namespace ZoomIntegrationMicroservice.Services
{
    public class MailServiceRepo:IMailService
    {
        private readonly IConfiguration _config;

        public MailServiceRepo(IConfiguration config)
        {
            _config = config;
        }
        public bool SendEmail(EmailModel email) {
            try
            {
                string fromAddress = _config["MailService:From"];
                var fromPassword = _config["MailService:To"];
                var message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Think Future Technology Pvt. Limited",fromAddress);
                message.From.Add(from);

                foreach (string toAddress in email.To) {
                    MailboxAddress to = new MailboxAddress("User Name", toAddress);
                    message.To.Add(to);
                }

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
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", email.File);
                    if (File.Exists(filePath)) {
                        bodyBuilder.Attachments.Add(filePath);
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
