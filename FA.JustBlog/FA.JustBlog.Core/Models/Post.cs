using FA.JustBlog.Core.Helper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Models
{
    public class Post
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? PostContent { get; set; }
        public string? UrlSlug
        {
            get { return SeoUrlHepler.FrientlyUrl(Title);
                    }
            set { }
        }
        public bool Published { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime Modified { get; set; }
        public int? ViewCount { get; set; }
        public int? RateCount { get; set; }
        public int? TotalRate { get; set; }
        [NotMapped]
        public decimal Rate
        {
            get { return Convert.ToDecimal(TotalRate / RateCount); }
        }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public virtual Category Category { get; set; }
        [ValidateNever]
        public virtual IList<PostTagMap> PostTags { get; set; }
        [ValidateNever]
        public virtual IList<Comment> Comments { get; set; }
    }
}
