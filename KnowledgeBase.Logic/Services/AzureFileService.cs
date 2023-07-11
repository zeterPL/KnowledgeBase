using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using KnowledgeBase.Logic.Dto;
using Microsoft.Extensions.Configuration;

namespace KnowledgeBase.Logic.Services;

public class AzureFileService
{
    private readonly BlobContainerClient _containerClient;

    public AzureFileService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FileStorage");

        var blobClient = new BlobServiceClient(connectionString);
        _containerClient = blobClient.GetBlobContainerClient("resources");
    }

    public async Task<BlobContentInfo> UploadFileAsync(UploadFileDto uploadFile)
    {
        var extension = Path.GetExtension(uploadFile.File.FileName);
        var fileName = $"{uploadFile.Name}{extension}";

        using var stream = new MemoryStream();
        await uploadFile.File.CopyToAsync(stream);
        stream.Position = 0;

        var response = await _containerClient.UploadBlobAsync(fileName, stream, default);
        return response;
    }
}