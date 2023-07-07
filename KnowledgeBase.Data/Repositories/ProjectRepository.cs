using KnowledgeBase.Data.Data;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Repositories;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(KnowledgeDbContext context) : base(context)
    {
    }

    public void SoftDelete(Project project)
    {
        project.IsDeleted = true;
        Update(project);
    }

    public Project? GetProjectWithPermissions(Guid id)
    {
        return _context.Set<Project>().Include(p => p.UsersPermissions).Single(p => p.Id == id);
    }
}