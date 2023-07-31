using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Dto
{
    public class ResourcePermissionDto
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public Guid UserId { get; set; }
        public ResourcePermissionName PermissionName { get; set; }
    }

    public static class UserResourcePermissionExtensions
    {
        public static ResourcePermissionDto ToPermissionDto(this UserResourcePermission perimission)
        {
            return new ResourcePermissionDto
            {
                Id = perimission.Id,
                ResourceId = perimission.ResourceId,
                UserId = perimission.UserId,
                PermissionName = perimission.Name,
            };
        }
    }
}
