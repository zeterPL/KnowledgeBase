namespace KnowledgeBase.Logic.Dto.PermissionsRequests;

public interface IReadPermissionRequestDto
{
    public UserDto Sender { get; set; }
    public UserDto Receiver { get; set; }
}