using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;

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

    public IEnumerable<Project> GetAllReadableByUser(Guid userId)
    {
        // Get all UserProject read permissions
        var userProjectPermissions = _context.Set<UserProjectPermission>()
            .Where(p => p.UserId == userId && p.PermissionName == ProjectPermissionName.ReadProject);

        // Get all projects which user can read
        var projects = GetSet()
            .Join(userProjectPermissions,
                project => project.Id,
                permission => permission.ProjectId,
                (project, permission) => project)
            .Where(project => project.IsDeleted == false);

        return projects;
    }

    public bool ProjectExists(Guid id)
    {
        return GetSet().Any(p => p.Id == id && !p.IsDeleted);
    }

    public bool ProjectExists(string name)
    {
        return GetSet().Any(p => p.Name == name);
    }

    public IEnumerable<Project> ProjectsExists(IEnumerable<string> names)
    {
        var existingProjects = GetSet().Where(p => names.Contains(p.Name));
        return existingProjects;
    }

    public async Task AddRangeAsync(IEnumerable<Project> projects)
    {
        await GetSet().AddRangeAsync(projects);
        await _context.SaveChangesAsync();
    }
}