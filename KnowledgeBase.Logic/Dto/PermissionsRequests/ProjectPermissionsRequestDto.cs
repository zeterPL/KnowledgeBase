using KnowledgeBase.Data.Models.Enums;
using Newtonsoft.Json;

namespace KnowledgeBase.Logic.Dto.PermissionsRequests;

public class ProjectPermissionsRequestDto
{
    public Guid SenderId { get; }
    public Guid ReceiverId { get; }
    public Guid ProjectId { get; }
    public IEnumerable<ProjectPermissionName> RequestedPermissions { get; }

    public ProjectPermissionsRequestDto(Guid senderId, Guid receiverId, Guid projectId,
        IEnumerable<ProjectPermissionName> requestedPermissions)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        ProjectId = projectId;
        RequestedPermissions = requestedPermissions;
    }

    public string ToJson()
    {
        var json = JsonConvert.SerializeObject(this, new Newtonsoft.Json.Converters.StringEnumConverter());
        return json;
    }
}