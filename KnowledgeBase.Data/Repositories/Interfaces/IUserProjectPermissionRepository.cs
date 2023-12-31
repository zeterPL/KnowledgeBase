﻿using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Data.Repositories.Interfaces;

public interface IUserProjectPermissionRepository : IGenericRepository<UserProjectPermission>
{
    public bool UserHasProjectPermission(Guid userId, Guid projectId, ProjectPermissionName permission);

    public void AddRange(IEnumerable<UserProjectPermission> permissions);

    public Task AddRangeAsync(IEnumerable<UserProjectPermission> permissions);
}