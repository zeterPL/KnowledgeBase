using KnowledgeBase.Data.Models.Enums;
using Newtonsoft.Json;

namespace KnowledgeBase.Logic.Dto.PermissionsRequests;

public class ProjectPermissionsRequestDto
{
    public Guid Sender { get; }
    public Guid Receiver { get; }
    public Guid ProjectId { get; }
    public IEnumerable<ProjectPermissionName> RequestedPermissions { get; }

    public ProjectPermissionsRequestDto(Guid sender, Guid receiver, Guid projectId,
        IEnumerable<ProjectPermissionName> requestedPermissions)
    {
        Sender = sender;
        Receiver = receiver;
        ProjectId = projectId;
        RequestedPermissions = requestedPermissions;
    }

    public string ToJson()
    {
        var json = JsonConvert.SerializeObject(this, new Newtonsoft.Json.Converters.StringEnumConverter());
        return json;
    }
}