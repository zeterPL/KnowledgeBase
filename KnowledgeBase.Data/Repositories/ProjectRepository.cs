using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Data.Repositories;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    private readonly DbSet<Project> _projects;

    public ProjectRepository(KnowledgeDbContext context) : base(context)
    {
        _projects = context.Set<Project>();
    }

    public void SoftDelete(Project project)
    {
        project.IsDeleted = true;
        Update(project);
    }

    public IEnumerable<Project> GetAllAssignedToUser(Guid userId)
    {
        return _projects.Where(p => p.AssignedUsers.Any(u => u.Id == userId));
    }

    public IEnumerable<Project> GetAllReadableByUser(Guid userId)
    {
        // Get all UserProject read permissions
        var userProjectPermissions = _context.Set<UserProjectPermission>()
            .Where(p => p.UserId == userId && p.PermissionName == ProjectPermissionName.ReadProject);

        // Get all projects which user can read
        var projects = _projects.Join(userProjectPermissions,
            project => project.Id,
            permission => permission.ProjectId,
            (project, permission) => project);

        // Get all projects assigned to user
        var assignedToUserProjects = GetAllAssignedToUser(userId);

        var result = projects.Union(assignedToUserProjects);
        return result;
    }
}