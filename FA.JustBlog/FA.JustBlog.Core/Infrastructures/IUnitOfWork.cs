using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Infrastructures
{
    public interface IUnitOfWork : IDisposable
    {
        public ICategoryRepository CategoryRepository { get; }
        public IPostRepository PostRepository { get; }
        public ITagRepository TagRepository { get; }
        public IPostTagMapRepository PostTagMapRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public int Save();
        public void Dispose();
    }
}
