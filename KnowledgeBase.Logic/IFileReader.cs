using KnowledgeBase.Logic.Dto.Project;

namespace KnowledgeBase.Logic;

public interface IFileReader
{
    IEnumerable<FileProjectDto> ReadProjects(Stream stream);
}