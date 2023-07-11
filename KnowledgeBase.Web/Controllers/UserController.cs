using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Logic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace KnowledgeBase.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;
        private readonly IProjectService _projectService;

        public UserController(IUserService userService, IRoleService roleService,
            IPermissionService permissionService, IProjectService projectService)
        {
            _userService = userService;
            _roleService = roleService;
            _permissionService = permissionService;
            _projectService = projectService;
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
            if (user is null)
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
            Guid newUserId;
            Guid roleId = Guid.Parse(Request.Form["Roles"]);
            user.RoleId = roleId;
            var role = _roleService.Get(roleId);

            if (ModelState.IsValid)
            {
                newUserId = _userService.AddUser(user);
                _userService.AssignPermissionBasedOnUserRole(role, newUserId);
                return RedirectToAction("List");
            }
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);

            var roles = _roleService.GetAll();
            ViewBag.RolesList = new SelectList(roles, "Id", "Name");
            return View(user);
        }

        [HttpGet]
        public IActionResult Permissions(Guid id)
        {
            ViewBag.UserId = id;
            var projects = _permissionService.GetPermissionsbyUserId(id).Select(p => p.ProjectId).Distinct();
            var perms = _permissionService.GetPermissionsbyUserId(id);
            List<PermissionViewModel> viewModels = new List<PermissionViewModel>();
            foreach (Guid project in projects)
            {
                var projctName = _projectService.Get(project).Name;
                var permsByProject = perms.Where(p => p.ProjectId == project).ToList();

                PermissionViewModel vm = new PermissionViewModel();
                vm.ProjectId = project;
                vm.ProjectName = projctName;
                vm.Permissions = permsByProject;
                viewModels.Add(vm);
            }

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult ManagePermission(
            Guid userId, Guid projectId)
        {
            var permissions = _permissionService.GetUserPermissionsByProjectIdAndUserId(userId, projectId);
            var projectName = _projectService.Get(projectId).Name;

            var viewModel = new PermissionViewModel();
            viewModel.ProjectId = projectId;
            viewModel.ProjectName = projectName;
            viewModel.Permissions = permissions.ToList();
            ViewBag.UserId = userId;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddPermission(Guid userId, Guid projectId)
        {
            var permissionOptions = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "ReadProject", Value = "0"},
                new SelectListItem {Text = "EditProject", Value = "1"},
                new SelectListItem {Text = "DeleteProject", Value = "2"},
            }, "Value", "Text");
            ViewBag.PermList = permissionOptions;
            ViewBag.UserId = userId;
            ViewBag.ProjectId = projectId;
            return View();
        }

        [HttpPost]
        public IActionResult AddPermission(PermissionDto permission)
        {
            var selectedName = int.Parse(Request.Form["Permissions"]);
            permission.PermissionName = (Data.Models.Enums.ProjectPermissionName)selectedName;

            if (ModelState.IsValid)
            {
                _permissionService.Add(permission);
                return RedirectToAction("ManagePermission", new { userId = permission.UserId, projectId = permission.ProjectId });
            }
            return View(permission);
        }

        [HttpGet]
        public IActionResult DeletePermission(Guid id)
        {
            var permission = _permissionService.GetById(id);
            var userId = permission.UserId;
            var projectId = permission.ProjectId;

            if (permission is null)
            {
                return NotFound();
            }
            _permissionService.Delete(permission);

            return RedirectToAction("ManagePermission", new { userId = userId, projectId = projectId });
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var user = _userService.GetById(id);
            if (user is null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(UserDto user)
        {
            if (ModelState.IsValid)
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
            if (user is null)
            {
                return NotFound();
            }

            _userService.Delete(user);

            return RedirectToAction("List");
        }
    }
}