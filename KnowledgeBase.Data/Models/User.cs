using Microsoft.AspNetCore.Identity;

namespace KnowledgeBase.Data.Models;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
    public virtual ICollection<Resource> Resources { get; set; }
    public virtual ICollection<UserProjectPermission> ProjectsPermissions { get; set; }
    public virtual ICollection<ProjectInterestedUser> ProjectInteresteds { get; set; }
    public virtual ICollection<UserResourcePermission> ResourcePermissions { get; set; }
    public virtual ICollection<ReportProjectIssue> ReportProjectsIssues { get; set; }
    public virtual ICollection<Project> ProjectsOwned { get; set; }
    public virtual ICollection<ProjectPermissionRequest> ProjectPermissionRequestsSended { get; set; }
    public virtual ICollection<ProjectPermissionRequest> ProjectPermissionRequestsReceived{ get; set; }
}