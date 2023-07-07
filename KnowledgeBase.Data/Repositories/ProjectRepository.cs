﻿using KnowledgeBase.Data.Data;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;

namespace KnowledgeBase.Data.Repositories;

public class ProjectRepository : GenericRepository<Project>, IGenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(KnowledgeDbContext context) : base(context)
    {
    }

    public void SoftDelete(Project project)
    {
        project.IsDeleted = true;
        Update(project);
    }
}