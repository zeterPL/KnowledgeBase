using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Logic.Dto
{
    public class PermissionDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public ProjectPermissionName PermissionName { get; set; }
    }

    public static class UserProjectPermissionExtensions
    {
        public static PermissionDto ToPermissionDto(this UserProjectPermission perimission)
        {
            return new PermissionDto
            {
                Id = perimission.Id,
                ProjectId = perimission.ProjectId,
                UserId = perimission.UserId,
                PermissionName = perimission.PermissionName,
            };
        }
    }
}