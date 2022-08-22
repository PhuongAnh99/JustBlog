using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Controllers
{
    [Area("User")]
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
