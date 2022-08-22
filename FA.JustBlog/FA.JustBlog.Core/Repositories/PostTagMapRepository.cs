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
    public class PostTagMapRepository : BaseRepository<PostTagMap>, IPostTagMapRepository
    {
        public PostTagMapRepository(JustBlogContext context) : base(context)
        {
        }
    }
}
