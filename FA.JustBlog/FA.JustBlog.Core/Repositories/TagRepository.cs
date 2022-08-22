using FA.JustBlog.Core.Data;
using FA.JustBlog.Core.Helper;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(JustBlogContext context) : base(context)
        {

        }

        public Tag GetTagByUrlSlug(string urlSlug)
        {
            return dbSet.FirstOrDefault(t => t.UrlSlug == urlSlug);
        }

        public IEnumerable<int> AddTagByString(string tags)
        {
            var tagNames = tags.Split(',');

            foreach (var tagName in tagNames)
            {
                var tagExisting = dbSet.Where(t => t.Name.Trim().ToLower() == tagName.Trim().ToLower()).Count();
                if (tagExisting == 0)
                {
                    var tag = new Tag()
                    {
                        Name = tagName,
                        UrlSlug = SeoUrlHepler.FrientlyUrl(tagName)
                    };
                    dbSet.Add(tag);

                }
            }

            context.SaveChanges();

            foreach (var tagName in tagNames)
            {
                var tagExisting = this.dbSet.FirstOrDefault(t => t.Name.Trim().ToLower() == tagName.Trim().ToLower());
                if (tagExisting != null)
                {
                    yield return tagExisting.Id;
                }
            }
        }
    }
}
