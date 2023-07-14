namespace KnowledgeBase.Logic.Dto;

public class DownloadResourceDto : ResourceDto
{
    public Stream Content { get; }
    public string ContentType { get; }

    public DownloadResourceDto(Stream content, string contentType)
    {
        Content = content;
        ContentType = contentType;
    }
}