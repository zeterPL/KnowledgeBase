﻿using KnowledgeBase.Data.Data;
using KnowledgeBase.Data.Models;

namespace KnowledgeBase.Data.Repositories;

public class ProjectRepository : GenericRepository<Project>, IGenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(KnowledgeDbContext context) : base(context)
    {
    }
}