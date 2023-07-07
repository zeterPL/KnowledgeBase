using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var users = _userService.GetAllUsers();
            return View(users.ToList());
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            UserDto user = new UserDto();
            user = _userService.GetById(id);

            return View(user);
        }
    }
}
