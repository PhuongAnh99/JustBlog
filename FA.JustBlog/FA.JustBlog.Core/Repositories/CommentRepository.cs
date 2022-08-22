using FA.JustBlog.Core.Data;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(JustBlogContext context) : base(context)
        {
        }

        public void AddComment(int postId, string commentName, string commentEmail, string commentTitle, string commentBody)
        {
            dbSet.Add(new Comment { PostId = postId, Name = commentName, Email = commentEmail, CommentHeader = commentTitle, CommentText = commentBody });
        }

        public IList<Comment> GetCommentsForPost(int postId)
        {
            return dbSet.Where(c => c.Post.Id == postId).ToList();
        }

        public IList<Comment> GetCommentsForPost(Post post)
        {
            return dbSet.Where(c => c.Post.Id == post.Id).ToList();
        }
    }
}
