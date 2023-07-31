using KnowledgeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Dto
{
    public class ProjectInterestedUserDto
    {
        public Guid Id { get; set; }    
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }

    public static class ProjectInterestedUserEstensions
    {
        public static ProjectInterestedUserDto ToProjectInterestedUserDto
            (this ProjectInterestedUser projectInterestedUser)
        {
            return new ProjectInterestedUserDto
            {
                Id = projectInterestedUser.Id,
                Description = projectInterestedUser.Description,
                UserId = projectInterestedUser.UserId,
                ProjectId = projectInterestedUser.ProjectId,
            };
        }
    }

}
