namespace KnowledgeBase.Logic.AzureServices.File;

public class DownloadAzureResourceFile : AzureResourceFile
{
    public Stream Stream { get; }
    public string ContentType { get; }

    public DownloadAzureResourceFile(Stream stream, string contentType)
    {
        Stream = stream;
        ContentType = contentType;
    }
}