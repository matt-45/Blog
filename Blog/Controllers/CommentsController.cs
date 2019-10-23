using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Blog.Models;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Author).Include(c => c.BlogPost);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName");
                ViewBag.BlogPostId = new SelectList(db.BlogPosts, "Id", "Title");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment, string commentBody)
        {
            if (ModelState.IsValid)
            {
                var parrentPost = db.BlogPosts.FirstOrDefault(p => p.Id == comment.BlogPostId);
                comment.Created = DateTime.UtcNow;
                comment.CommentBody = commentBody;
                comment.AuthorId = User.Identity.GetUserId();
                comment.Author = db.Users.FirstOrDefault(u => u.Id == comment.AuthorId);
                comment.BlogPostId = parrentPost.Id;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "BlogPosts", new { slug = parrentPost.Slug });
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorId);
            ViewBag.BlogPostId = new SelectList(db.BlogPosts, "Id", "Title", comment.BlogPostId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorId);
            ViewBag.BlogPostId = new SelectList(db.BlogPosts, "Id", "Title", comment.BlogPostId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BlogPostId,AuthorId,CommentBody")] Comment comment)
        {
            

            if (ModelState.IsValid)
            {
                var com = db.Comments.First(c => c.Id == comment.Id);
                var parrentPost = db.BlogPosts.First(p => p.Id == comment.BlogPostId);

                if (String.IsNullOrWhiteSpace(comment.CommentBody))
                {
                    ModelState.AddModelError("CommentBody", "Invalid title");
                    return View(comment);
                }
                comment.Created = DateTime.UtcNow;
                com.Updated = DateTime.UtcNow;
                com.CommentBody = comment.CommentBody;
                db.SaveChanges();
                return RedirectToAction("Details", "BlogPosts", new { slug = parrentPost.Slug});
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorId);
            ViewBag.BlogPostId = new SelectList(db.BlogPosts, "Id", "Title", comment.BlogPostId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int parrentId)
        {
            var parrentPost = db.BlogPosts.First(p => p.Id == parrentId);
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "BlogPosts", new { slug = parrentPost.Slug });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
