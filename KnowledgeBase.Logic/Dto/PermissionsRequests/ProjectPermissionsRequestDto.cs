using KnowledgeBase.Data.Models.Enums;
using Newtonsoft.Json;

namespace KnowledgeBase.Logic.Dto.PermissionsRequests;

public class ProjectPermissionsRequestDto
{
    public string SenderName { get; }
    public string ReceiverEmail { get; }
    public string ProjectName { get; }
    public IEnumerable<ProjectPermissionName> RequestedPermissions { get; }

    public ProjectPermissionsRequestDto(string senderName, string receiverEmail, string projectName,
        IEnumerable<ProjectPermissionName> requestedPermissions)
    {
        SenderName = senderName;
        ReceiverEmail = receiverEmail;
        ProjectName = projectName;
        RequestedPermissions = requestedPermissions;
    }

    public string ToJson()
    {
        var json = JsonConvert.SerializeObject(this, new Newtonsoft.Json.Converters.StringEnumConverter());
        return json;
    }
}