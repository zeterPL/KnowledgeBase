using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.Dto.Resources.AzureResource;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.ResourceHandlers;

public class AzureResourceHandler : IResourceHandler
{
    private readonly IAzureStorageService _azureStorageService;

    public AzureResourceHandler(IAzureStorageService azureStorageService)
    {
        _azureStorageService = azureStorageService;
    }

    public Type GetResourceType()
    {
        return typeof(AzureResourceDto);
    }

    private async Task<AzureResourceDto> UploadFile(string name, Guid projectId, IFormFile? file)
    {
        if (file == null)
        {
            throw new ArgumentException("File cant be null");
        }

        var uploadFile = new UploadAzureResourceFile(name, projectId, file);
        var azureResourceFile = await _azureStorageService.UploadFileAsync(uploadFile);

        return new AzureResourceDto
        {
            AzureStorageAbsolutePath = azureResourceFile.AzureStoragePath,
            AzureFileName = azureResourceFile.AzureFileName,
        };
    }

    public async Task<Resource> UpdateDetailsAsync<T>(T dto, Resource model)
        where T : IResourceAction
    {
        if (dto is not AzureResourceDto resourceDto)
        {
            throw new ArgumentException("Invalid resource types for this handler");
        }

        if (resourceDto.File == null)
        {
            return model;
        }

        var result = await UploadFile(resourceDto.Name, resourceDto.ProjectId, resourceDto.File);

        var resource = (AzureResource)model;
        resource.AzureFileName = result.AzureFileName!;
        resource.AzureStorageAbsolutePath = result.AzureStorageAbsolutePath!;

        return resource;
    }
}