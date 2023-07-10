using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IPermissionService
{
    public bool UserHadProjectPermission(Guid userId, Guid projectId, PermissionName permission);
    public IList<PermissionDto> GetPermissionsbyUserId(Guid userId);
}