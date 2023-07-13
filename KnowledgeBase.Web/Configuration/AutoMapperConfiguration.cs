using KnowledgeBase.Logic.AutoMapper;

namespace KnowledgeBase.Web.Configuration;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ResourceMapper), typeof(ProjectMapper));

        return services;
    }
}