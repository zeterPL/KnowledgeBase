using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.Dto.Resources.AzureResource;
using KnowledgeBase.Logic.ResourceHandlers;
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
        services.AddScoped<IAzureStorageService, AzureStorageService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITagService, TagService>();

        services.AddScoped<IResourceHandler<AzureResource,AzureResourceDto, CreateAzureResourceDto>, AzureResourceHandler>();

        return services;
    }
}