using Blog.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Helpers;
using System.Net;
using PagedList;
using PagedList.Mvc;



// These are different .NET frameworks.

namespace Blog.Controllers // all controllers are inside this namespace
{
    public class HomeController : Controller { 
/*var data = new dataVM()
            {
                data.
            }*/
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var blogPosts = db.BlogPosts.Where(p => p.Published).AsQueryable();
            return View(blogPosts.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailFormModel model = new EmailFormModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var mailMessage = new MailMessage();
                mailMessage.To.Add(new MailAddress("mattpark102@outlook.com"));
                mailMessage.From = new MailAddress("mattpark102@outlook.com");
                mailMessage.Subject = "Message from blog post user.";
                mailMessage.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                mailMessage.IsBodyHtml = true;
                ModelState.AddModelError("Message", "Message has been sent.");
                SendEmail.Send(mailMessage);
                return View(model);
            }
            return View(model);
        }

    }
}