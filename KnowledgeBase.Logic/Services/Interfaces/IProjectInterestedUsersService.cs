using KnowledgeBase.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services.Interfaces
{
    public interface IProjectInterestedUserService
    {
        public void AddInterestedUsersToSpecificProjectByUsersIds(IList<Guid> usersIds, Guid projectId);
        public ProjectInterestedUserDto GetInterestedUserByUserIdAndProjectId(Guid userId, Guid projectId);
        public void Update(ProjectInterestedUserDto interestedDto);
        public void Delete(ProjectInterestedUserDto interestedDto);
    }
}
