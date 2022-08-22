using FA.JustBlog.Core.Helper;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Models.ViewModels;
using FA.JustBlog.Core.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_BlogOwner + ", " + SD.Role_Contributor)]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string postFilter)
        {
            var postList = _unitOfWork.PostRepository.GetAll(includeProperties: "Category");
            ViewData["TitlePost"] = "All Post";
            switch (postFilter)
            {
                case "lastest":
                    postList = _unitOfWork.PostRepository.GetLatestPost(5);
                    ViewData["TitlePost"] = "Lastes Post";
                    break;
                case "viewed":
                    postList = _unitOfWork.PostRepository.GetMostViewedPost(5);
                    ViewData["TitlePost"] = "Most Viewed Post";
                    break;
                case "interesting":
                    postList = _unitOfWork.PostRepository.GetHighestPosts(5);
                    ViewData["TitlePost"] = "Most Interesting Post";
                    break;
                case "published":
                    postList = _unitOfWork.PostRepository.GetPublishedPosts();
                    ViewData["TitlePost"] = "Published Post";
                    break;
                case "unpublished":
                    postList = _unitOfWork.PostRepository.GetUnpublishedPosts();
                    ViewData["TitlePost"] = "Un-Published Post";
                    break;
            }
            return View(postList);
        }
        //GET
        public IActionResult Create()
        {
            PostVM postVM = new()
            {
                Post = new(),
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
            };
            return View(postVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostVM request)
        {

            if (ModelState.IsValid)
            {
                //1. add tag vao database
                var tagIds = _unitOfWork.TagRepository.AddTagByString(request.Tags);
                // 2. create postTag
                var postTags = new List<PostTagMap>();
                foreach (var tagId in tagIds)
                {
                    var postTag = new PostTagMap()
                    {
                        TagId = tagId
                    };
                    postTags.Add(postTag);
                }
                var post = new Post()
                {
                    Title = request.Post.Title,
                    PostContent = request.Post.PostContent,
                    CategoryId = request.Post.CategoryId,
                    ShortDescription = request.Post.ShortDescription,
                    PostTags = postTags,
                    Published = request.Post.Published,
                    ViewCount = request.Post.ViewCount,
                    RateCount = request.Post.RateCount,
                    TotalRate = request.Post.TotalRate
                };
                _unitOfWork.PostRepository.Add(post);
                _unitOfWork.Save();
                TempData["success"] = "Post created successfully";
                return RedirectToAction("Index");
            }
            
            return View(request);
        }

        public IActionResult Update(int id)
        {
            PostVM postVM = new()
            {
                Post = new(),
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
            };
            postVM.Post = _unitOfWork.PostRepository.Find(id);
            return View(postVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PostVM request)
        {

            if (ModelState.IsValid)
            {
                //1. add tag vao database
                var tagIds = _unitOfWork.TagRepository.AddTagByString(request.Tags);
                // 2. create postTag
                var postTags = new List<PostTagMap>();
                foreach (var tagId in tagIds)
                {
                    var postTag = new PostTagMap()
                    {
                        TagId = tagId
                    };
                    postTags.Add(postTag);
                }
                _unitOfWork.PostRepository.Update(request.Post);
                _unitOfWork.Save();
                TempData["success"] = "Post updated successfully";
                return RedirectToAction("Index");
            }

            return View(request);
        }
        //GET
        [Authorize(Roles = SD.Role_BlogOwner)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var postFromDb = _unitOfWork.PostRepository.GetFirstOrDefault(c => c.Id == id, "Category");
            if (postFromDb == null)
            {
                return NotFound();
            }
            return View(postFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.PostRepository.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.PostRepository.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
