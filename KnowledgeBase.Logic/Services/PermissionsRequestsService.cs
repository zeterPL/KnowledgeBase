using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Dto.PermissionsRequests;
using KnowledgeBase.Logic.Dto.Project;

namespace KnowledgeBase.Logic.Services;

public class PermissionsRequestsService
{
    private readonly IPermissionRequestRepository _permissionRequestRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserProjectPermissionRepository _userProjectPermissionRepository;
    private readonly IMapper _mapper;

    public PermissionsRequestsService(IPermissionRequestRepository permissionRequestRepository, IMapper mapper,
        IProjectRepository projectRepository, IUserProjectPermissionRepository userProjectPermissionRepository)
    {
        _permissionRequestRepository = permissionRequestRepository;
        _mapper = mapper;
        _projectRepository = projectRepository;
        _userProjectPermissionRepository = userProjectPermissionRepository;
    }

    public IEnumerable<ApproveProjectPermissionRequestDto> GetProjectPermissionsRequestsSendToUser(Guid userId)
    {
        var requests = _permissionRequestRepository.GetRequestsReceivedByUser<ProjectPermissionRequest>(userId);
        var grouped = requests.GroupBy(r => new { r.Sender, r.ProjectId }).ToList();
        var projectsIds = grouped.Select(g => g.Key.ProjectId);
        var projects = _projectRepository.GetProjects(projectsIds);

        var result = grouped.Select(g => new ApproveProjectPermissionRequestDto
        {
            Sender = g.Key.Sender.ToUserDto(),
            Project = _mapper.Map<ProjectDto>(projects.Single(p => p.Id == g.Key.ProjectId)),
            RequestedPermissions = g.Select(p => p.RequestedPermission),
        }).ToList();

        return result;
    }

    public void ApproveProjectPermissionsRequests(Guid projectId, Guid senderId, Guid receiverId)
    {
        var requests = _permissionRequestRepository
            .GetRequestsSendByUser<ProjectPermissionRequest>(senderId)
            .Where(p => p.ProjectId == projectId && p.ReceiverId == receiverId)
            .ToList();

        var newPermissions = requests.Select(r => new UserProjectPermission
        {
            PermissionName = r.RequestedPermission,
            UserId = senderId,
            ProjectId = projectId,
        });

        _userProjectPermissionRepository.AddRange(newPermissions);

        foreach (var request in requests)
        {
            request.IsDeleted = true;
        }

        _permissionRequestRepository.UpdateRange(requests);
    }
}