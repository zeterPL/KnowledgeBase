namespace KnowledgeBase.Logic.Dto;

public class DownloadResourceFileDto : ResourceFileDto
{
    public Stream Stream { get; init; }
    public string ContentType { get; set; }

    public DownloadResourceFileDto(Stream stream, string contentType)
    {
        Stream = stream;
        ContentType = contentType;
    }
}