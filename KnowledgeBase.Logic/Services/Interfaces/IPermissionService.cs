using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IPermissionService
{
    public bool UserHadProjectPermission(Guid userId, Guid projectId, PermissionName permission);
    public IList<PermissionDto> GetPermissionsbyUserId(Guid userId);
    public PermissionDto GetPermissionsbyProjectId(Guid projectId);
    public IList<PermissionDto> GetUserPermissionsByProjectIdAndUserId(Guid userId, Guid projectId);
    public PermissionDto GetById(Guid id);
    public void Delete(PermissionDto permission);
    public void Add(PermissionDto permission);
    public bool UserHasProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission);
}