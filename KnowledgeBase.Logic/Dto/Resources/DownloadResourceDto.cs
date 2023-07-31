using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;

namespace KnowledgeBase.Logic.Dto.Resources;

public class DownloadResourceDto : IResourceDto
{
    public Stream Content { get; }
    public string ContentType { get; }
    public string FileName { get; set; }

    public DownloadResourceDto(Stream content, string contentType, string fileName)
    {
        Content = content;
        ContentType = contentType;
        FileName = fileName;
    }

    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
    public ResourceCategory Category { get; set; }
    public Guid UserId { get; set; }
}