using KnowledgeBase.Data.Models;
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



namespace KnowledgeBase.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddPermissionsByUserIdAndRoleId(Guid userId, Guid roleId)
        {
            _userRepository.AddPermissionsByUserIdAndRoleId(userId, roleId);
        }

        public void AddUser(UserDto userDto)
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

            _userRepository.Add(user);

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
