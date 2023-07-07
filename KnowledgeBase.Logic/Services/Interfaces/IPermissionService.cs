using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Logic.Services.Interfaces;

public interface IPermissionService
{
    public bool UserHadProjectPermission(Guid userId, Guid projectId, PermissionName permission);
}