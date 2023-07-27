using KnowledgeBase.Data.Repositories;
using KnowledgeBase.Data.Repositories.Interfaces;

namespace KnowledgeBase.Web.Configuration;

public static class RepositoryInjectionConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IUserProjectPermissionRepository, UserProjectPermissionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IProjectTagRepository, ProjectTagRepository>();
        services.AddScoped<IProjectInterestedUserRepository, ProjectInterestedUserRepository>();
        services.AddScoped<IUserResourcePermissionRepository, UserResourcePermissionRepository>();
        services.AddScoped<IReportProjectIssueRepository, ReportProjectIssueRepository>();

        return services;
    }
}