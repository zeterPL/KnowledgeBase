using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.Dto.Resources.AzureResource;
using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.ResourceHandlers;

public class AzureResourceHandler : AbstractResourceHandler<AzureResourceActionDto, AzureResource>
{
    private readonly IAzureStorageService _azureStorageService;

    public AzureResourceHandler(IAzureStorageService azureStorageService, IMapper mapper) : base(mapper)
    {
        _azureStorageService = azureStorageService;
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

    protected override async Task<Resource> HandleUpdateDetails(AzureResourceActionDto dto, AzureResource model)
    {
        if (dto.File == null)
        {
            return model;
        }

        var result = await UploadFile(dto.Name, dto.ProjectId, dto.File);

        model.AzureFileName = result.AzureFileName!;
        model.AzureStorageAbsolutePath = result.AzureStorageAbsolutePath!;

        return model;
    }
}