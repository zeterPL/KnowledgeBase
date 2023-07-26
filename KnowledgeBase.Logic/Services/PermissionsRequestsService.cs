using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto.PermissionsRequests;

namespace KnowledgeBase.Logic.Services;

public class PermissionsRequestsService
{
    private readonly IPermissionRequestRepository _permissionRequestRepository;

    public PermissionsRequestsService(IPermissionRequestRepository permissionRequestRepository)
    {
        _permissionRequestRepository = permissionRequestRepository;
    }

    public IEnumerable<ApproveProjectPermissionRequestDto> GetProjectPermissionsRequestsSendToUser(Guid id)
    {
        return new List<ApproveProjectPermissionRequestDto>();
    }
}