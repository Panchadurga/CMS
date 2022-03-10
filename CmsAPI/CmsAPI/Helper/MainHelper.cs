using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CmsAPI.Helper
{

    public class MainHelper
    {
        //To access the data from appsettings.json file
        private IConfiguration configuration;
        //public MainHelper(IConfiguration _configuration)
        //{
        //    configuration = _configuration;
        //}

        public string Send(string from, string to, string subject, string body)
        {
            try
            {
               
                var host = "smtp.gmail.com";
                var port = 587;

                var username = "manifortestpurpose@gmail.com";
                var password = "Flowerbell123$";
                var enable = true;
                var smtpClient = new SmtpClient
                {
                    UseDefaultCredentials = false,
                    Host = host,
                    Port = port,                                     
                    EnableSsl = enable,
                    Credentials = new NetworkCredential(username, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                var mailMessage = new MailMessage(from, to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);
                return "success";
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return e.Message;
            }
        }
    }
}
