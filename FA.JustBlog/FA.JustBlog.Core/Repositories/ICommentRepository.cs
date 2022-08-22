using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        public void AddComment(int postId, string commentName, string commentEmail, string commentTitle, string commentBody);
        public IList<Comment> GetCommentsForPost(int postId);
        public IList<Comment> GetCommentsForPost(Post post);
    }
}
