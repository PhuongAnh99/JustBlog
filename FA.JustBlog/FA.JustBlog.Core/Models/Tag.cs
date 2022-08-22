using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? UrlSlug { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        [ValidateNever]
        public virtual IList<PostTagMap> TagPosts { get; set; }
    }
}
