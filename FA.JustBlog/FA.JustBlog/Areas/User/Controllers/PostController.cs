using FA.JustBlog.Core.Data;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FA.JustBlog.Controllers
{
    [Area("User")]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var postList = _unitOfWork.PostRepository.GetAll(includeProperties: "Category");
            return View(postList);
        }
        public IActionResult Details(int id)
        {
            var postFromDb = _unitOfWork.PostRepository.Find(id);
            return View(postFromDb);
        }
        [Route("Post/{year}/{month}/{title}")]
        public IActionResult Details(int year, int month, string title)
        {
            var postFromDb = _unitOfWork.PostRepository.FindPost(year, month, title);
            return View(postFromDb);
        }
        public IList<Post> LastestPost()
        {
            var latestPosts = _unitOfWork.PostRepository.GetLatestPost(3);
            return latestPosts;
        }
        public IActionResult MostViewedPosts()
        {
            ViewData["MostViewedPosts"] = _unitOfWork.PostRepository.GetMostViewedPost(3);
            return PartialView("_Layout");
        }

        public IActionResult GetPostsByCategory(string category)
        {
            var postList = _unitOfWork.PostRepository.GetAll(u => u.Category.Name == category, "Category");
            return View("Index", postList);
        }
        public IActionResult GetPostsByTag(string tag)
        {
            var postList = _unitOfWork.PostRepository.GetPostsByTag(tag);
            return View("Index", postList);
        }
    }
}
