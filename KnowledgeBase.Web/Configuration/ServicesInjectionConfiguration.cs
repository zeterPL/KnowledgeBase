using KnowledgeBase.Logic.Services;
using KnowledgeBase.Logic.Services.Interfaces;

namespace KnowledgeBase.Web.Configuration;

public static class ServicesInjectionConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IResourceService, ResourceService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<AzureFileService>();

        return services;
    }
}