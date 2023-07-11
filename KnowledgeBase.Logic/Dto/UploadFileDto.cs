using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.Dto;

public class UploadFileDto
{
    public string Name { get; init; }

    public IFormFile File { get; init; }

    public UploadFileDto(string name, IFormFile file)
    {
        Name = name;
        File = file;
    }
}