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

        return services;
    }
}