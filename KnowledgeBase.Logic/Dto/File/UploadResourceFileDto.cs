using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.Dto;

public class UploadResourceFileDto : ResourceFileDto
{
    public IFormFile File { get; init; }

    public UploadResourceFileDto(string name, string projectName, IFormFile file)
    {
        Name = name;
        ProjectName = projectName;
        File = file;
    }
}