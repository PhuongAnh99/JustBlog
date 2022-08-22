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
    public class CategoryRepositoryTests
    {
        private Mock<JustBlogContext> _context;
        private Mock<DbSet<Category>> _dbSet;
        private CategoryRepository _categoryRepository;
        [SetUp]
        public void Setup()
        {
            var data = new List<Category>
            {
                new Category{Id = 1, Name = "Cate 1", UrlSlug = "cate1.com.vn", Description = "None 1"},
                new Category{Id = 2, Name = "Cate 2", UrlSlug = "cate2.com.vn", Description = "None 2"},
                new Category{Id = 3, Name = "Cate 3", UrlSlug = "cate3.com.vn", Description = "None 3"}
            }.AsQueryable();

            _dbSet = new Mock<DbSet<Category>>();
            _dbSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            _dbSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            _dbSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _dbSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context = new Mock<JustBlogContext>();
            _context.Setup(c => c.Set<Category>()).Returns(_dbSet.Object);

            _categoryRepository = new CategoryRepository(_context.Object);
        }

        [Test]
        public void GetAll_WhenCalled_ReturnAllCategoriesList()
        {
            var categories = _categoryRepository.GetAll();

            Assert.AreEqual(3, categories.Count);
            Assert.AreEqual("Cate 1", categories[0].Name);
            Assert.AreEqual("Cate 2", categories[1].Name);
            Assert.AreEqual("Cate 3", categories[2].Name);
        }

        [Test]
        public void Find_WhenCalled_ReturnCategoryById()
        {
            Category category = _categoryRepository.Find(2);

            Assert.IsNotNull(category);
            Assert.AreEqual("Cate 2", category.Name);
            Assert.AreEqual("cate2.com.vn", category.UrlSlug);
            Assert.AreEqual("None 2", category.Description);
        }
    }
}