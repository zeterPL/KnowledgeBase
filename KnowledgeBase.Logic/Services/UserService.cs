﻿using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IProjectRepository projectRepository, 
            IPermissionRepository permissionRepository, IRoleRepository roleRepository)
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
            List<Permission> permissions = new List<Permission>();
            if (roleName == UserRoles.SuperAdmin.ToString())
            {
                Permission perm = new Permission
                {
                    UserId = userId,
                    ProjectId = projectId,
                    PermissionName = PermissionName.ReadProject
                };
                permissions.Add(perm);

                Permission perm1 = new Permission
                {
                    UserId = userId,
                    ProjectId = projectId,
                    PermissionName = PermissionName.EditProject
                };
                permissions.Add(perm1);

                Permission perm2 = new Permission
                {
                    UserId = userId,
                    ProjectId = projectId,
                    PermissionName = PermissionName.DeleteProject
                };
                permissions.Add(perm2);
            }
            if(roleName == UserRoles.Admin.ToString())
            {
                Permission perm = new Permission
                {
                    UserId = userId,
                    ProjectId = projectId,
                    PermissionName = PermissionName.ReadProject
                };
                permissions.Add(perm);

                Permission perm1 = new Permission
                {
                    UserId = userId,
                    ProjectId = projectId,
                    PermissionName = PermissionName.EditProject
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
                List<Permission> permissions = new List<Permission>();
                if (role.Name == "SuperAdmin")
                {
                    
                    Permission perm = new Permission
                    {
                        ProjectId = project.Id,
                        UserId = userId,
                        PermissionName = Data.Models.Enums.PermissionName.ReadProject
                    };
                    permissions.Add(perm);

                    Permission perm1 = new Permission
                    {
                        ProjectId = project.Id,
                        UserId = userId,
                        PermissionName = Data.Models.Enums.PermissionName.EditProject
                    };
                    permissions.Add(perm1);

                    Permission perm2 = new Permission
                    {
                        ProjectId = project.Id,
                        UserId = userId,
                        PermissionName = Data.Models.Enums.PermissionName.DeleteProject
                    };
                    permissions.Add(perm2);

                }
                else if(role.Name == "Admin")
                {
                    Permission perm = new Permission
                    {
                        ProjectId = project.Id,
                        UserId = userId,
                        PermissionName = Data.Models.Enums.PermissionName.ReadProject
                    };
                    permissions.Add(perm);

                    Permission perm1 = new Permission
                    {
                        ProjectId = project.Id,
                        UserId = userId,
                        PermissionName = Data.Models.Enums.PermissionName.EditProject
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
