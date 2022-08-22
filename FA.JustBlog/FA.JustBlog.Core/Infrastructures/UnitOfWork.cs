using FA.JustBlog.Core.Data;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JustBlogContext _dbContext;

        public UnitOfWork(JustBlogContext dbContext)
        {
            _dbContext = dbContext;
            CategoryRepository = new CategoryRepository(_dbContext);
            PostRepository = new PostRepository(_dbContext);
            TagRepository = new TagRepository(_dbContext);
            PostTagMapRepository = new PostTagMapRepository(_dbContext);
            CommentRepository = new CommentRepository(_dbContext);
        }

        public ICategoryRepository CategoryRepository { get; private set; }
        public IPostRepository PostRepository { get; private set; }
        public ITagRepository TagRepository { get; private set; }
        public IPostTagMapRepository PostTagMapRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}
