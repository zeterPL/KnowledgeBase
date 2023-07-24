using KnowledgeBase.Logic.Dto.Project;

namespace KnowledgeBase.Logic.Exceptions;

public class ProjectException : Exception
{
}

public class ProjectsExistsInDatabaseException : ProjectException
{
    public IEnumerable<ProjectDto> Projects { get; }

    public ProjectsExistsInDatabaseException(IEnumerable<ProjectDto> projects)
    {
        Projects = projects;
    }
}