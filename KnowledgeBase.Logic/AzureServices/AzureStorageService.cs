using Azure;
using Azure.Storage.Blobs;
using KnowledgeBase.Logic.AzureServices.File;
using Microsoft.Extensions.Configuration;

namespace KnowledgeBase.Logic.AzureServices;

public class AzureStorageService
{
    private readonly BlobContainerClient _blobContainerClient;

    public AzureStorageService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FileStorage");

        var blobClient = new BlobServiceClient(connectionString);
        _blobContainerClient = blobClient.GetBlobContainerClient("resources");
    }

    public async Task<AzureResourceFile> UploadFileAsync(UploadAzureResourceFile uploadAzureResourceFile)
    {
        var extension = Path.GetExtension(uploadAzureResourceFile.File.FileName);
        var absolutePath = $"{uploadAzureResourceFile.ProjectName}/{uploadAzureResourceFile.ResourceName}{extension}";

        using var stream = new MemoryStream();
        await uploadAzureResourceFile.File.CopyToAsync(stream);
        stream.Position = 0;

        try
        {
            var response = await _blobContainerClient.UploadBlobAsync(absolutePath, stream, default);
            uploadAzureResourceFile.AzureStoragePath = absolutePath;
            uploadAzureResourceFile.AzureFileName = $"{uploadAzureResourceFile.ResourceName}{extension}";
            return uploadAzureResourceFile;
        }
        catch (RequestFailedException ex)
        {
            throw ex;
        }
    }

    public async Task<DownloadAzureResourceFile> DownloadFileAsync(AzureResourceFile azureResourceFile)
    {
        var blob = _blobContainerClient.GetBlobClient(azureResourceFile.AzureStoragePath);

        if (!await blob.ExistsAsync())
        {
            throw new FileNotFoundException("File with given path was not found on azure storage");
        }

        try
        {
            var downloaded = await blob.DownloadStreamingAsync();
            var fileResponse = new DownloadAzureResourceFile(downloaded.Value.Content, downloaded.Value.Details.ContentType);
            return fileResponse;
        }
        catch (RequestFailedException ex)
        {
            throw ex;
        }
    }
}