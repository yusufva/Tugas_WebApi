﻿using System.Net.Mail;
using WebApi.Contracts;

namespace WebApi.Utilities.Handler
{
    public class EmailHandler : IEmailHandler
    {
        private string _server;
        private int _port;
        private string _fromEmailAddress;

        public EmailHandler(string server, int port, string fromEmailAddress)
        {
            _server = server;
            _port = port;
            _fromEmailAddress = fromEmailAddress;
        }

        public void Send(string subject, string body, string toEmail)
        {
            var message = new MailMessage()
            {
                From = new MailAddress(_fromEmailAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            }; //instance email

            message.To.Add(new MailAddress(toEmail)); //add tujuan email

            using var smtpClient = new SmtpClient(_server, _port); //instance smtp client
            smtpClient.Send(message); //ngirim

        }
    }
}
