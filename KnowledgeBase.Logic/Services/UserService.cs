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

        public bool AddUser(UserDto userDto)
        {
            User user = new User
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                UserName = userDto.UserName,
                EmailConfirmed = true
            };
            var hashedPass = new PasswordHasher<object>().HashPassword(null, userDto.Password);
            user.PasswordHash = hashedPass;
            

            try
            {
                _userRepository.Add(user);
            }
            catch (Exception ex) { return false; }

            return true;
        }

        public bool Delete(UserDto user)
        {
            throw new NotImplementedException();
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

        public UserDto Update(UserDto user)
        {
            throw new NotImplementedException();
        }

      
    }
}
