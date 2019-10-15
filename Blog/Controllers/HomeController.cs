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

    }
}