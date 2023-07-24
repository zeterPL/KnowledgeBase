﻿using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.ViewModels
{
    public class UserAccountViewModel
    {
        public UserDto User { get; set; }
        public RoleDto Role { get; set; }
    }
}
