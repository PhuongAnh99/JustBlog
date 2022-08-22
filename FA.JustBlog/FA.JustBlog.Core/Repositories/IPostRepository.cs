using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        public Post FindPost(int year, int month, string urlString);
        public IList<Post> GetPublishedPosts();
        public IList<Post> GetUnpublishedPosts();
        public IList<Post> GetLatestPost(int size);
        public IList<Post> GetPostsByMonth(DateTime monthYear);
        public int CountPostsForCategory(string category);
        public IList<Post> GetPostsByCategory(string category);
        public int CountPostsForTag(string tag);
        public IList<Post> GetPostsByTag(string tag);
        public IList<Post> GetMostViewedPost(int size);
        public IList<Post> GetHighestPosts(int size);
        public IList<Tag> GetTagsByPost(int id);
    }
}
