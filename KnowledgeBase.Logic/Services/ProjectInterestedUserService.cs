using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
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

        public void Delete(ProjectInterestedUserDto interestedDto)
        {
            var tmp = _projectInterestedUserRepository.Get(interestedDto.Id);
            _projectInterestedUserRepository.Remove(tmp);
        }

        public ProjectInterestedUserDto? GetInterestedUserByUserIdAndProjectId(Guid userId, Guid projectId)
        {
            var interested = _projectInterestedUserRepository.GetAll().Where(pu => pu.UserId == userId && pu.ProjectId == projectId)
                .Select(pu => pu.ToProjectInterestedUserDto()).FirstOrDefault();
            if (interested == null) return null;
            return interested;
        }

        public void Update(ProjectInterestedUserDto interestedDto)
        {
            ProjectInterestedUser interested = new ProjectInterestedUser
            {
                Id = interestedDto.Id,
                ProjectId = interestedDto.ProjectId,
                UserId = interestedDto.UserId,
                Description = interestedDto.Description,
            };
            _projectInterestedUserRepository.Update(interested);
        }
    }
}
