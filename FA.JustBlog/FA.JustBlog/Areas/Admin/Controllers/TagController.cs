using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_BlogOwner + ", " + SD.Role_Contributor)]
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Tag> objTagList = _unitOfWork.TagRepository.GetAll();
            return View(objTagList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag obj)
        {
            //Server-side Validation
            if (ModelState.IsValid)
            {
                _unitOfWork.TagRepository.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Tag created successfully.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var tagFromDb = _unitOfWork.TagRepository.GetFirstOrDefault(c => c.Id == id);
            if (tagFromDb == null)
            {
                return NotFound();
            }
            return View(tagFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag obj)
        {
            //Server-side Validation
            if (ModelState.IsValid)
            {
                _unitOfWork.TagRepository.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Tag updated successfully.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        [Authorize(Roles = SD.Role_BlogOwner)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var tagFromDb = _unitOfWork.TagRepository.GetFirstOrDefault(c => c.Id == id);
            if (tagFromDb == null)
            {
                return NotFound();
            }
            return View(tagFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.TagRepository.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.TagRepository.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "Tag deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
