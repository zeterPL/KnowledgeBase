using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto.Project;

namespace KnowledgeBase.Logic.Dto.PermissionsRequests;

public class ApproveProjectPermissionRequestDto : IReadPermissionRequestDto
{
    public UserDto Sender { get; set; }
    public UserDto Receiver { get; set; }
    public ProjectDto Project { get; set; }
    public IEnumerable<ProjectPermissionName> RequestedPermissions { get; set; }
}