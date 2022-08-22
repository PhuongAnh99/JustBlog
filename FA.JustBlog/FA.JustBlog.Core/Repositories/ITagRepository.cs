﻿using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        public Tag GetTagByUrlSlug(string urlSlug);
        public IEnumerable<int> AddTagByString(string tags);
    }
}
