﻿using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Data.Repositories.Interfaces;

public interface IPermissionRepository : IGenericRepository<Permission>
{
    public void AddRange(List<Permission> permissions);
}
