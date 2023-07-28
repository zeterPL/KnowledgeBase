using KnowledgeBase.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers
{
    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || User is null)
                return View();
            else return RedirectToAction("MainPage");
        }

        [Authorize]
        public IActionResult MainPage()
        {
            return View();
        }

        [Authorize(Policy = UserRolesTypes.SuperAdmin)]
        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
