using KnowledgeBase.Logic.AzureServices.File;

namespace KnowledgeBase.Logic.AzureServices;

public interface IAzureStorageService
{
    Task<AzureResourceFile> UploadFileAsync(UploadAzureResourceFile uploadAzureResourceFile);
    Task<DownloadAzureResourceFile> DownloadFileAsync(AzureResourceFile azureResourceFile);
}