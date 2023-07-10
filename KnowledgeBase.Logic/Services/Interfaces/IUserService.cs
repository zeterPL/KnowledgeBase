﻿using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserDto> GetAllUsers();
        public UserDto GetById(Guid id);
        public void AddUser(UserDto user);
        public UserDto Update(UserDto user);
        public bool Delete(UserDto user);
        public bool SoftDelete(UserDto user);
        public IList<PermissionDto> GetAllUserPermissions(Guid id);
        public void AddPermissionsByUserIdAndRoleId(Guid userId, Guid roleId);
    }
}
