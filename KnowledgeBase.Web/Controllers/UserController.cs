using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KnowledgeBase.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;

        public UserController(IUserService userService, IRoleService roleService, IPermissionService permissionService)
        {
            _userService = userService;
            _roleService = roleService;
            _permissionService = permissionService;
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
            RoleDto role = _roleService.Get(user.RoleId);
            ViewBag.Role = role;
            if(user is null)
            {
                return NotFound();
            }
            var perms = _permissionService.GetPermissionsbyUserId(id);
            ViewBag.Permissions = perms;

            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roles = _roleService.GetAll();
            ViewBag.RolesList = new SelectList(roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDto user)
        {
            Guid roleId = Guid.Parse(Request.Form["Roles"]);
            user.RoleId = roleId;
         

            if (ModelState.IsValid)
            {
                _userService.AddUser(user);
                return RedirectToAction("List");
            }
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);

            var roles = _roleService.GetAll();
            ViewBag.RolesList = new SelectList(roles, "Id", "Name");
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var user = _userService.GetById(id);
            if(user is null)
            {
                return NotFound();
            }
         
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(UserDto user)
        {           
            if(ModelState.IsValid)
            {
               
                _userService.Update(user);
                return RedirectToAction("List");
            }
                    
            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var user = _userService.GetById(id);
            if(user is null)
            {
                return NotFound();
            }

            _userService.Delete(user);

            return RedirectToAction("List");
        }
    }
}
