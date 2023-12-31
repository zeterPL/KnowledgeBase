﻿using KnowledgeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Data.Repositories.Interfaces
{
    public interface IProjectInterestedUserRepository : IGenericRepository<ProjectInterestedUser>
    {
        public void AddRange(IList<ProjectInterestedUser> projectInterestedUsers);
    }
}
