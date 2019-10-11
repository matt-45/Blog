using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.ViewModels
{
    
    public class DataViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public DataViewModel()
        {
            this.Comments = new HashSet<Comment>();
            this.BlogPosts = new HashSet<BlogPost>();
        }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}