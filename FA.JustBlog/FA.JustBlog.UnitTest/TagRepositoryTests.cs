using FA.JustBlog.Core.Data;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FA.JustBlog.UnitTest
{
    public class TagRepositoryTests
    {
        private Mock<JustBlogContext> _context;
        private Mock<DbSet<Tag>> _dbSet;
        private TagRepository _tagRepository;
        [SetUp]
        public void Setup()
        {
            var data = new List<Tag>
            {
                new Tag{Id = 1, Name = "Tag 1", UrlSlug = "tag1.com.vn", Description = "None", Count = 2},
                new Tag{Id = 2, Name = "Tag 2", UrlSlug = "tag2.com.vn", Description = "None", Count = 1},
                new Tag{Id = 3, Name = "Tag 3", UrlSlug = "tag3.com.vn", Description = "None", Count = 3}
            }.AsQueryable();

            _dbSet = new Mock<DbSet<Tag>>();
            _dbSet.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(data.Provider);
            _dbSet.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(data.Expression);
            _dbSet.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _dbSet.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context = new Mock<JustBlogContext>();
            _context.Setup(c => c.Set<Tag>()).Returns(_dbSet.Object);

            _tagRepository = new TagRepository(_context.Object);
        }

        [Test]
        public void GetAll_WhenCalled_ReturnAllTagsList()
        {
            var tags = _tagRepository.GetAll();

            Assert.AreEqual(3, tags.Count);
            Assert.AreEqual("Tag 1", tags[0].Name);
            Assert.AreEqual("Tag 2", tags[1].Name);
            Assert.AreEqual("Tag 3", tags[2].Name);
        }

        [Test]
        public void Find_WhenCalled_ReturnTagById()
        {
            Tag tag = _tagRepository.Find(2);

            Assert.IsNotNull(tag);
            Assert.AreEqual("Tag 2", tag.Name);
            Assert.AreEqual("tag2.com.vn", tag.UrlSlug);
            Assert.AreEqual(1, tag.Count);
        }

        [Test]
        public void GetTagByUrlSlug_WhenCalled_ReturnTagByByUrlSlug()
        {
            Tag tag = _tagRepository.GetTagByUrlSlug("tag1.com.vn");

            Assert.AreEqual("Tag 1", tag.Name);
            Assert.AreEqual("tag1.com.vn", tag.UrlSlug);
            Assert.AreEqual("None", tag.Description);
            Assert.AreEqual(2, tag.Count);
        }
    }
}