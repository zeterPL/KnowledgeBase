using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;
        private readonly IProjectService _projectService;

        public UserApiController(IUserService userService, IRoleService roleService,
            IPermissionService permissionService, IProjectService projectService)
        {
            _userService = userService;
            _roleService = roleService;
            _permissionService = permissionService;
            _projectService = projectService;
        }

        [Route("/UserController")]
        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            return _userService.GetAllUsers();
        }

        [Route("/UserController/OwnerId=")]
        [HttpGet]
        public IActionResult Get([FromQuery] string id)
        {
            return Ok(_userService.GetById(Guid.Parse(id)));
        }

        [Route("/UserController/Roles")]
        [HttpGet]
        public IActionResult GetRoles()
        {
            return Ok(_roleService.GetAll());
        }


        [Route("/UserController/Permissions/OwnerId={Id}")]
        [HttpGet]
        public IActionResult GetPermissionsByUser([FromQuery] string Id)
        {
            return Ok(_permissionService.GetPermissionsbyUserId(Guid.Parse(Id)));
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserDto user)
        {
            user.Id = Guid.NewGuid();
            return Ok(_userService.AddUser(user));
        }

        [HttpPut]
        public IActionResult Edit([FromBody] UserDto UserDto)
        {
            return Ok(_userService.Update(UserDto));
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] UserDto UserDto)
        {
            return Ok(_userService.SoftDelete(UserDto));
        }
    }
}
