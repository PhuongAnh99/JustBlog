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
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IList<Comment> cmtList = _unitOfWork.CommentRepository.GetAll(includeProperties: "Post");
            return View(cmtList);
        }
        [Authorize(Roles = SD.Role_BlogOwner)]
        public IActionResult Create()
        {
            CommentVM commentVM = new()
            {
                Comment = new(),
                PostList = _unitOfWork.PostRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Title,
                    Value = u.Id.ToString()
                })
            };
            return View(commentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CommentVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CommentRepository.Add(obj.Comment);
                _unitOfWork.Save();
                TempData["success"] = "Comment created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int id)
        {
            CommentVM commentVM = new()
            {
                Comment = _unitOfWork.CommentRepository.Find(id),
                PostList = _unitOfWork.PostRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Title,
                    Value = u.Id.ToString()
                })
            };
            return View(commentVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CommentVM request)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CommentRepository.Update(request.Comment);
                _unitOfWork.Save();
                TempData["success"] = "Comment updated successfully";
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
            var commentFromDb = _unitOfWork.CommentRepository.GetFirstOrDefault(c => c.Id == id, "Post");
            if (commentFromDb == null)
            {
                return NotFound();
            }
            return View(commentFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CommentRepository.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CommentRepository.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "Comment deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
