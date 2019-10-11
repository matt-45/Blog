using Blog.Models;
using Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class DataController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Data
        public ActionResult Index()
        {
            var DataVM = new DataViewModel
            {
                BlogPosts = db.BlogPosts.ToList(),
                Comments = db.Comments.ToList()
            };
            return View(DataVM);
        }
    }
}