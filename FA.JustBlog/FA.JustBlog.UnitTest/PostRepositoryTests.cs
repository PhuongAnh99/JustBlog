using FA.JustBlog.Core.Data;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FA.JustBlog.UnitTest
{
    public class PostRepositoryTests
    {
        private Mock<JustBlogContext> _context;
        private Mock<DbSet<Category>> _dbSetCategory;
        private Mock<DbSet<Post>> _dbSetPost;
        private Mock<DbSet<Tag>> _dbSetTag;
        private PostRepository _postRepository;
        [SetUp]
        public void Setup()
        {
            var cate1 = new Category { Id = 1, Name = "Cate 1", UrlSlug = "cate1.com.vn", Description = "None 1" };
            var cate2 = new Category { Id = 2, Name = "Cate 2", UrlSlug = "cate2.com.vn", Description = "None 2" };
            var cate3 = new Category { Id = 3, Name = "Cate 3", UrlSlug = "cate3.com.vn", Description = "None 3" };
            //Mock Categories Data
            var categoryData = new List<Category>
            {
                cate1,
                cate2,
                cate3
            }.AsQueryable();

            _dbSetCategory = new Mock<DbSet<Category>>();
            _dbSetCategory.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(categoryData.Provider);
            _dbSetCategory.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categoryData.Expression);
            _dbSetCategory.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categoryData.ElementType);
            _dbSetCategory.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categoryData.GetEnumerator());

            //Mock Posts Data
            var post1 = new Post { Id = 1, Title = "Post 1", UrlSlug = "post1.com.vn", Published = true, PostedOn = new DateTime(2022, 6, 22), Modified = new DateTime(2022, 6, 30), Category = cate1 };
            var post2 = new Post { Id = 2, Title = "Post 2", UrlSlug = "post2.com.vn", Published = false, PostedOn = new DateTime(2022, 5, 20), Modified = new DateTime(2022, 5, 30), Category = cate2 };
            var post3 = new Post { Id = 3, Title = "Post 3", UrlSlug = "post3.com.vn", Published = true, PostedOn = new DateTime(2022, 6, 21), Modified = new DateTime(2022, 6, 30), Category = cate1 };
            var post4 = new Post { Id = 4, Title = "Post 4", UrlSlug = "post4.com.vn", Published = true, PostedOn = new DateTime(2022, 5, 20), Modified = new DateTime(2022, 5, 30), Category = cate2 };
            var post5 = new Post { Id = 5, Title = "Post 5", UrlSlug = "post5.com.vn", Published = false, PostedOn = new DateTime(2022, 6, 20), Modified = new DateTime(2022, 6, 30), Category = cate1 };
            var post6 = new Post { Id = 6, Title = "Post 6", UrlSlug = "post6.com.vn", Published = true, PostedOn = new DateTime(2022, 4, 20), Modified = new DateTime(2022, 4, 30), Category = cate3 };

            var postData = new List<Post>
            {
                post1,
                post2,
                post3,
                post4,
                post5,
                post6
            }.AsQueryable();

            _dbSetPost = new Mock<DbSet<Post>>();
            _dbSetPost.As<IQueryable<Post>>().Setup(m => m.Provider).Returns(postData.Provider);
            _dbSetPost.As<IQueryable<Post>>().Setup(m => m.Expression).Returns(postData.Expression);
            _dbSetPost.As<IQueryable<Post>>().Setup(m => m.ElementType).Returns(postData.ElementType);
            _dbSetPost.As<IQueryable<Post>>().Setup(m => m.GetEnumerator()).Returns(postData.GetEnumerator());

            //Mock Tags Data
            var tag1 = new Tag { Id = 1, Name = "Tag 1", UrlSlug = "tag1.com.vn", Description = "None", Count = 2 };
            var tag2 = new Tag { Id = 2, Name = "Tag 2", UrlSlug = "tag2.com.vn", Description = "None", Count = 1 };
            var tag3 = new Tag { Id = 3, Name = "Tag 3", UrlSlug = "tag3.com.vn", Description = "None", Count = 3 };
            var tagData = new List<Tag>
            {
                new Tag { Id = 1, Name = "Tag 1", UrlSlug = "tag1.com.vn", Description = "None", Count = 2, TagPosts = new List<PostTagMap>() { new PostTagMap() { Post = post1, Tag = tag1 },
                                                                                                                                                new PostTagMap() { Post = post2, Tag = tag1 },
                                                                                                                                                new PostTagMap() { Post = post4, Tag = tag1 }} },
                new Tag { Id = 2, Name = "Tag 2", UrlSlug = "tag2.com.vn", Description = "None", Count = 1, TagPosts = new List<PostTagMap>() { new PostTagMap() { Post = post5, Tag = tag2 }} },
                new Tag { Id = 3, Name = "Tag 3", UrlSlug = "tag3.com.vn", Description = "None", Count = 3, TagPosts = new List<PostTagMap>() { new PostTagMap() { Post = post4, Tag = tag3 },
                                                                                                                                                new PostTagMap() { Post = post2, Tag = tag3 }} }

            }.AsQueryable();

            _dbSetTag = new Mock<DbSet<Tag>>();
            _dbSetTag.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(tagData.Provider);
            _dbSetTag.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(tagData.Expression);
            _dbSetTag.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(tagData.ElementType);
            _dbSetTag.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator()).Returns(tagData.GetEnumerator());

            _context = new Mock<JustBlogContext>();
            _context.Setup(c => c.Set<Category>()).Returns(_dbSetCategory.Object);
            _context.Setup(c => c.Set<Post>()).Returns(_dbSetPost.Object);
            _context.Setup(c => c.Tags).Returns(_dbSetTag.Object);

            _postRepository = new PostRepository(_context.Object);
        }

        [Test]
        public void GetAll_WhenCalled_ReturnAllPostsList()
        {
            var posts = _postRepository.GetAll();

            Assert.AreEqual(6, posts.Count);
        }

        [Test]
        public void Find_WhenCalled_ReturnPostById()
        {
            Post post = _postRepository.Find(2);

            Assert.IsNotNull(post);
            Assert.AreEqual("Post 2", post.Title);
        }

        [Test]
        public void FindPost_WhenCalled_ReturnPostByYearMonthUrlSlug()
        {
            Post post = _postRepository.FindPost(2022, 5, "post2.com.vn");

            Assert.IsNotNull(post);
            Assert.AreEqual("Post 2", post.Title);
        }

        [Test]
        public void GetLatestPost_WhenCalled_ReturnLatestPostsBySize()
        {
            var posts = _postRepository.GetLatestPost(3);

            Assert.IsNotNull(posts);
            Assert.AreEqual("Post 1", posts[0].Title);
            Assert.AreEqual("Post 3", posts[1].Title);
            Assert.AreEqual("Post 5", posts[2].Title);
        }

        [Test]
        public void GetPostsByMonth_WhenCalled_ReturnPostsByMonth()
        {
            var posts = _postRepository.GetPostsByMonth(new DateTime(2022, 6, 25));

            Assert.IsNotNull(posts);
            Assert.AreEqual(3, posts.Count);
        }

        [Test]
        public void GetPublishedPosts_WhenCalled_ReturnPublishedPosts()
        {
            var posts = _postRepository.GetPublishedPosts();

            Assert.IsNotNull(posts);
            Assert.AreEqual(4, posts.Count);
        }

        [Test]
        public void GetUnpublishedPosts_WhenCalled_ReturnUnpublishedPosts()
        {
            var posts = _postRepository.GetUnpublishedPosts();

            Assert.IsNotNull(posts);
            Assert.AreEqual(2, posts.Count);
        }

        [Test]
        public void GetPostsByTag_WhenCalled_ReturnPostsListByTag()
        {
            var posts = _postRepository.GetPostsByTag("Tag 1");

            Assert.IsNotNull(posts);
            Assert.AreEqual(3, posts.Count);
        }

        [Test]
        public void GetPostsByCategory_WhenCalled_ReturnPostsListByCategory()
        {
            var posts = _postRepository.GetPostsByCategory("Cate 1");

            Assert.IsNotNull(posts);
            Assert.AreEqual(3, posts.Count);
        }

        [Test]
        public void CountPostsForCategory_WhenCalled_ReturnPostsNumberByCategory()
        {
            int result = _postRepository.CountPostsForCategory("Cate 1");
            Assert.AreEqual(3, result);
        }

        [Test]
        public void CountPostsForTag_WhenCalled_ReturnPostsNumberByTag()
        {
            int result = _postRepository.CountPostsForTag("Tag 1");
            Assert.AreEqual(3, result);
        }
    }
}
