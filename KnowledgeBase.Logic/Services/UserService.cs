using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeBase.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserProjectPermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IProjectRepository projectRepository,
            IUserProjectPermissionRepository permissionRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
        }

        public void AddPermisionsToSpecificProject(Guid projectId, Guid userId)
        {
            var user = _userRepository.Get(userId);
            var role = _roleRepository.Get(user.RoleId);
            var roleName = role.Name;
            List<UserProjectPermission> permissions = new List<UserProjectPermission>();
            if (roleName == UserRoles.SuperAdmin.ToString())
            {
                UserProjectPermission perm = new UserProjectPermission
                {
                    UserId = userId,
                    ProjectId = projectId,
                    PermissionName = ProjectPermissionName.ReadProject
                };
                permissions.Add(perm);

                UserProjectPermission perm1 = new UserProjectPermission
                {
                    UserId = userId,
                    ProjectId = projectId,
                    PermissionName = ProjectPermissionName.EditProject
                };
                permissions.Add(perm1);

                UserProjectPermission perm2 = new UserProjectPermission
                {
                    UserId = userId,
                    ProjectId = projectId,
                    PermissionName = ProjectPermissionName.DeleteProject
                };
                permissions.Add(perm2);
            }
            if (roleName == UserRoles.Admin.ToString())
            {
                UserProjectPermission perm = new UserProjectPermission
                {
                    UserId = userId,
                    ProjectId = projectId,
                    PermissionName = ProjectPermissionName.ReadProject
                };
                permissions.Add(perm);

                UserProjectPermission perm1 = new UserProjectPermission
                {
                    UserId = userId,
                    ProjectId = projectId,
                    PermissionName = ProjectPermissionName.EditProject
                };
                permissions.Add(perm1);
            }
            _permissionRepository.AddRange(permissions);
        }

        public void AddPermissionsByUserIdAndRoleId(Guid userId, Guid roleId)
        {
            _userRepository.AddPermissionsByUserIdAndRoleId(userId, roleId);
        }

        public Guid AddUser(UserDto userDto)
        {
            User user = new User
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                UserName = userDto.Email,
                EmailConfirmed = true,
                NormalizedEmail = userDto.Email.ToUpper(),
                NormalizedUserName = userDto.Email.ToUpper(),
                RoleId = userDto.RoleId,
            };
            var hashedPass = new PasswordHasher<object>().HashPassword(null, userDto.Password);
            user.PasswordHash = hashedPass;

            var securityStamp = Guid.NewGuid().ToString("D").ToUpper();
            user.SecurityStamp = securityStamp;

            return _userRepository.Add(user);
        }

        public void AssignPermissionBasedOnUserRole(RoleDto role, Guid userId)
        {
            var allProjects = _projectRepository.GetAll();
            foreach (var project in allProjects)
            {
                List<UserProjectPermission> permissions = new List<UserProjectPermission>();
                if (role.Name == "SuperAdmin")
                {
                    UserProjectPermission perm = new UserProjectPermission
                    {
                        ProjectId = project.Id,
                        UserId = userId,
                        PermissionName = Data.Models.Enums.ProjectPermissionName.ReadProject
                    };
                    permissions.Add(perm);

                    UserProjectPermission perm1 = new UserProjectPermission
                    {
                        ProjectId = project.Id,
                        UserId = userId,
                        PermissionName = Data.Models.Enums.ProjectPermissionName.EditProject
                    };
                    permissions.Add(perm1);

                    UserProjectPermission perm2 = new UserProjectPermission
                    {
                        ProjectId = project.Id,
                        UserId = userId,
                        PermissionName = Data.Models.Enums.ProjectPermissionName.DeleteProject
                    };
                    permissions.Add(perm2);
                }
                else if (role.Name == "Admin")
                {
                    UserProjectPermission perm = new UserProjectPermission
                    {
                        ProjectId = project.Id,
                        UserId = userId,
                        PermissionName = Data.Models.Enums.ProjectPermissionName.ReadProject
                    };
                    permissions.Add(perm);

                    UserProjectPermission perm1 = new UserProjectPermission
                    {
                        ProjectId = project.Id,
                        UserId = userId,
                        PermissionName = Data.Models.Enums.ProjectPermissionName.EditProject
                    };
                    permissions.Add(perm1);
                }
                _permissionRepository.AddRange(permissions);
            }
        }

        public bool Delete(UserDto userDto)
        {
            var user = _userRepository.Get(userDto.Id);
            if (user == null) { return false; }

            _userRepository.Remove(user);
            return true;
        }

        public IList<PermissionDto> GetAllUserPermissions(Guid id)
        {
            var permissions = _userRepository.GetAllUserPermissionsByUserId(id);
            return permissions.Select(perm => perm.ToPermissionDto()).ToList();
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return users.Select(u => u.ToUserDto());
        }

        public UserDto GetById(Guid id)
        {
            var user = _userRepository.Get(id).ToUserDto();
            return user;
        }

        public bool SoftDelete(UserDto user)
        {
            throw new NotImplementedException();
        }

        public UserDto Update(UserDto userDto)
        {
            var user = _userRepository.Get(userDto.Id);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;

            user.Email = userDto.Email;

            _userRepository.Update(user);
            return user.ToUserDto();
        }
    }
}