namespace KnowledgeBase.Logic.Dto.Resources;

public class DownloadResourceDto : ResourceDto
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
}