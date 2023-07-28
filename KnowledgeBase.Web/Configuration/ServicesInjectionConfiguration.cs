using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.Interfaces;
using KnowledgeBase.Logic.ResourceHandlers;
using KnowledgeBase.Logic.Services;
using KnowledgeBase.Logic.Services.Interfaces;

namespace KnowledgeBase.Web.Configuration;

public static class ServicesInjectionConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAzureStorageService, AzureStorageService>();
        services.AddScoped<IAzureServiceBusHandler, AzureServiceBusHandler>();
        
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IResourceService, ResourceService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IProjectInterestedUserService, ProjectInterestedUserService>();
        services.AddScoped<IUserResourcePermissionService, UserResourcePermissionService>();
        services.AddScoped<IReportProjectIssueService, ReportProjectIssueService>();

        services.AddScoped<PermissionsRequestsService>();

        services.AddScoped<IResourceHandler, AzureResourceHandler>();
        services.AddScoped<IResourceHandler, NoteResourceHandler>();
        services.AddScoped<IResourceHandler, CredentialsResourceHandler>();
        services.AddScoped<ResourceHandlersManager>();

        return services;
    }
}