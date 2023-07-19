using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services
{
    public class ProjectInterestedUserService : IProjectInterestedUserService
    {
        private readonly IProjectInterestedUserRepository _projectInterestedUserRepository;

        public ProjectInterestedUserService(IProjectInterestedUserRepository projectInterestedUserRepository)
        {
            _projectInterestedUserRepository = projectInterestedUserRepository;
        }

        public void AddInterestedUsersToSpecificProjectByUsersIds(IList<Guid> usersIds, Guid projectId)
        {
            List<ProjectInterestedUser> newInterestedUsers = new List<ProjectInterestedUser>();
            foreach(var userId in usersIds)
            {
                ProjectInterestedUser user = new ProjectInterestedUser
                {
                    UserId = userId,
                    ProjectId = projectId,
                    Description = "Empty description"
                };
                newInterestedUsers.Add(user);
            }
            _projectInterestedUserRepository.AddRange(newInterestedUsers);
        }
    }
}
