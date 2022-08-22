using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Data
{
    public class JustBlogContext : IdentityDbContext
    {
        public JustBlogContext(DbContextOptions<JustBlogContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTagMap>(entity =>
            {
                entity.HasKey(pt => new { pt.PostId, pt.TagId });
            });

            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
        //entities
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<PostTagMap> PostTags { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
