using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.Dto.Resources.AzureResource;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Logic.ResourceHandlers;

public class AzureResourceHandler : IResourceHandler
{
    private readonly IAzureStorageService _azureStorageService;
    private readonly IResourceRepository _resourceRepository;
    private readonly IMapper _mapper;

    public AzureResourceHandler(IAzureStorageService azureStorageService, IMapper mapper,
        IResourceRepository resourceRepository)
    {
        _azureStorageService = azureStorageService;
        _mapper = mapper;
        _resourceRepository = resourceRepository;
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

    public async Task<Guid> AddAsync<T>(T dto) where T : ICreateResourceDto
    {
        if (dto is not CreateAzureResourceDto resourceDto)
        {
            throw new ArgumentException();
        }

        await UploadFile(resourceDto.Name, resourceDto.ProjectId, resourceDto.File);
        var resource = _mapper.Map<AzureResource>(resourceDto);
        _resourceRepository.Add(resource);
        return resource.Id;
    }

    public async Task UpdateAsync<T>(T dto) where T : IUpdateResourceDto
    {
        if (dto is not UpdateAzureResourceDto resourceDto)
        {
            throw new ArgumentException();
        }

        var resource = (AzureResource)_resourceRepository.Get(dto.Id)!;

        resource.Name = resourceDto.Name;
        resource.Description = resourceDto.Description;

        if (resourceDto.File == null)
        {
            _resourceRepository.Update(resource);
            return;
        }

        var result = await UploadFile(resourceDto.Name, resourceDto.ProjectId, resourceDto.File);

        resource.AzureFileName = result.AzureFileName!;
        resource.AzureStorageAbsolutePath = result.AzureStorageAbsolutePath!;

        _resourceRepository.Update(resource);
    }

    public Type GetResourceType()
    {
        return typeof(AzureResourceDto);
    }
}