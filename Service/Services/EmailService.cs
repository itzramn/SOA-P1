using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.IServices;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly string[] Scopes = { GmailService.Scope.GmailSend };
        private readonly string ApplicationName = "SOA-P1";
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendSmtpEmail(string recipient, string subject, string body)
        {
            string senderEmail = configuration["GmailConfig:SenderSmtpEmail"];
            string senderName = configuration["GmailConfig:SenderSmtpName"];
            string senderPassword = configuration["GmailConfig:SenderSmtpPassword"];

            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderEmail, senderName);
            message.To.Add(recipient);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.Send(message);
        }

        public void SendEmail(string recipient, string subject, string body)
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            System.Diagnostics.Debug.WriteLine($"Adress: {recipient}");
            var message = CreateMessage(configuration["GmailConfig:SenderEmail"], recipient, subject, body);
            SendMessage(service, "me", message);
        }

        private Message CreateMessage(string sender, string recipient, string subject, string body)
        {
            var message = new Message();

            var plainTextBytes = Encoding.UTF8.GetBytes(body);
            var base64PlainText = Convert.ToBase64String(plainTextBytes);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"From: {sender}");
            stringBuilder.AppendLine($"To: {recipient}");
            stringBuilder.AppendLine($"Subject: {subject}");
            stringBuilder.AppendLine("Content-Type: text/html; charset=utf-8");
            stringBuilder.AppendLine("Content-Transfer-Encoding: base64");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(base64PlainText);

            var rawMessage = stringBuilder.ToString();
            message.Raw = Base64UrlEncode(rawMessage);

            return message;
        }

        private void SendMessage(GmailService service, string userId, Message emailMessage)
        {
            service.Users.Messages.Send(emailMessage, userId).Execute();
        }

        private string Base64UrlEncode(string input)
        {
            System.Diagnostics.Debug.WriteLine($"Adress: {input}");
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var base64 = Convert.ToBase64String(inputBytes);
            var base64Url = base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
            return base64Url;
        }
    }
}