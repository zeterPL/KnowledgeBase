using KnowledgeBase.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers
{
    
    public class HomeController : Controller
    {
        
        public IActionResult Index()
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
