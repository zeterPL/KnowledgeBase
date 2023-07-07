using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services;
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
            return RedirectToAction("List");
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDto user)
        {
            if(ModelState.IsValid)
            {
                _userService.AddUser(user);
                return RedirectToAction("List");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var user = _userService.GetById(id);
         
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(UserDto user)
        {           
            if(ModelState.IsValid)
            {
                user.UserName = user.Email;
                _userService.Update(user);
                return RedirectToAction("List");
            }
                    
            return View(user);
        }
    }
}
