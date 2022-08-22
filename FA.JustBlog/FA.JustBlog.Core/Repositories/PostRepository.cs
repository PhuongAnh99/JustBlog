using FA.JustBlog.Core.Data;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(JustBlogContext context) : base(context)
        {
        }
        public int CountPostsForCategory(string category)
        {
            return dbSet.Where(p => p.Category.Name == category).Count();
        }
        public int CountPostsForTag(string tag)
        {
            return context.Tags.Where(x => x.Name == tag).SelectMany(x => x.TagPosts).Count();
        }

        public Post FindPost(int year, int month, string urlString)
        {
            return dbSet.FirstOrDefault(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug == urlString)!;
        }

        public IList<Post> GetHighestPosts(int size)
        {
            return dbSet.OrderByDescending(x => (x.TotalRate / x.RateCount)).Take(size).ToList();
        }

        public IList<Post> GetLatestPost(int size)
        {
            return dbSet.OrderByDescending(p => p.PostedOn).Take(size).ToList();
        }

        public IList<Post> GetMostViewedPost(int size)
        {
            return dbSet.OrderByDescending(x => x.ViewCount).Take(size).ToList();
        }

        public IList<Post> GetPostsByCategory(string category)
        {
            return dbSet.Where(p => p.Category.Name == category).ToList();
        }

        public IList<Post> GetPostsByMonth(DateTime monthYear)
        {
            return dbSet.Where(p => p.PostedOn.Month == monthYear.Month && p.PostedOn.Year == monthYear.Year).ToList();
        }

        public IList<Post> GetPostsByTag(string tag)
        {
            return context.Tags.Where(x => x.Name == tag).SelectMany(x => x.TagPosts.Select(x => x.Post)).Include(p=>p.Category).ToList();
        }

        public IList<Tag> GetTagsByPost(int id)
        {
            return context.Posts.Where(u => u.Id == id).SelectMany(x => x.PostTags).Select(x => x.Tag).ToList();
        }

        public IList<Post> GetPublishedPosts()
        {
            return dbSet.Where(x => x.Published).ToList();
        }

        public IList<Post> GetUnpublishedPosts()
        {
            return dbSet.Where(x => x.Published == false).ToList();
        }
    }
}
