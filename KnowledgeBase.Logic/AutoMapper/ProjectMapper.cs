using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto.Project;

namespace KnowledgeBase.Logic.AutoMapper;

public class ProjectMapper : Profile
{
    public ProjectMapper()
    {
        CreateMap<ProjectDto, Project>();
        CreateMap<Project, ProjectDto>();
    }
}