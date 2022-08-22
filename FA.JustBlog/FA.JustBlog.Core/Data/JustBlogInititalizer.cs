using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Data
{
    public static class JustBlogInititalizer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Cate 1", UrlSlug = "abc1.com.vn", Description = "abc" },
                new Category() { Id = 2, Name = "Cate 2", UrlSlug = "abc2.com.vn", Description = "abc" },
                new Category() { Id = 3, Name = "Cate 3", UrlSlug = "abc3.com.vn", Description = "abc" }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post() { Id = 1, Title = "Cong chua", ShortDescription = "abc", PostContent = "Cong chua bong dem", Published = true, PostedOn = DateTime.Now, Modified = DateTime.Now, CategoryId = 1 },
                new Post() { Id = 2, Title = "Em be", ShortDescription = "abc", PostContent = "Em be moi lon xinh gai", Published = false, PostedOn = DateTime.Now, Modified = DateTime.Now, CategoryId = 2 },
                new Post() { Id = 3, Title = "Hoang tu", ShortDescription = "abc", PostContent = "Hoang tu bong dem", Published = true, PostedOn = DateTime.Now, Modified = DateTime.Now, CategoryId = 3 },
                new Post() { Id = 4, Title = "Dep trai", ShortDescription = "abc", PostContent = "Dep trai thi rat tot", Published = false, PostedOn = DateTime.Now, Modified = DateTime.Now, CategoryId = 1 },
                new Post() { Id = 5, Title = "Xinh gai", ShortDescription = "abc", PostContent = "Xinh gai nhieu nguoi theo", Published = true, PostedOn = DateTime.Now, Modified = DateTime.Now, CategoryId = 2 }
            );

            modelBuilder.Entity<Tag>().HasData(
                new Tag() { Id = 1, Name = "Hay", UrlSlug = "abc", Description = "abc", Count = 2 },
                new Tag() { Id = 2, Name = "Dung", UrlSlug = "abc", Description = "abc", Count = 3 },
                new Tag() { Id = 3, Name = "Tot", UrlSlug = "abc", Description = "abc", Count = 1 }
            );

            modelBuilder.Entity<PostTagMap>().HasData(
                new PostTagMap() { PostId = 1, TagId = 2 },
                new PostTagMap() { PostId = 2, TagId = 1 },
                new PostTagMap() { PostId = 4, TagId = 3 },
                new PostTagMap() { PostId = 5, TagId = 2 },
                new PostTagMap() { PostId = 4, TagId = 1 },
                new PostTagMap() { PostId = 2, TagId = 3 }
            );
        }
    }
}
