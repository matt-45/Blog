using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Blog.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Blog.Helpers
{
    public class SendEmail
    {
        public static void Send(MailMessage mailMessage)
        {
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "mattpark102@outlook.com",
                    Password = "vdbRyPd#F5$8"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);
            }
        }
    }
}