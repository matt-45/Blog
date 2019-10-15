using Blog.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

// These are different .NET frameworks.

namespace Blog.Controllers // all controllers are inside this namespace
{
    public class HomeController : Controller { 
/*var data = new dataVM()
            {
                data.
            }*/
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            var blogPosts = db.BlogPosts.Where(b => b.Published).ToList();

            return View(blogPosts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact (EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email from: <bold>{0}</bold> ({1})</p><p>Message:</p><p>{2}</p>";
                    var from = "MyPortfolio<example@email.com>";
                    //model.Body = "This is a message from Matthew's Portfolio.";

                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = model.Subject,
                        Body = string.Format(body, model.FromName, model.FromEmail, model.Body),
                        IsBodyHtml = true
                        
                    };
                    Debug.WriteLine("Email has been created");
                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);

                    return View(new EmailModel());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            Debug.WriteLine("Something wrong with model state.");
            return View(model);
        }

    }
}