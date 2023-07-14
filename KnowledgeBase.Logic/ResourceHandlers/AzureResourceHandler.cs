using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.Dto.Resources.AzureResource;

namespace KnowledgeBase.Logic.ResourceHandlers;

public class AzureResourceHandler : IResourceHandler<AzureResource, AzureResourceDto, CreateAzureResourceDto>
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

    private async Task UploadFile(AzureResourceDto resourceDto)
    {
        if (resourceDto.File == null)
        {
            throw new ArgumentException("File cant be null");
        }

        var uploadFile = new UploadAzureResourceFile(resourceDto.Name, resourceDto.ProjectId, resourceDto.File);
        var azureResourceFile = await _azureStorageService.UploadFileAsync(uploadFile);

        resourceDto.AzureStorageAbsolutePath = azureResourceFile.AzureStoragePath;
        resourceDto.AzureFileName = azureResourceFile.AzureFileName;
    }

    public async Task<Guid> AddAsync(CreateAzureResourceDto resourceDto)
    {
        await UploadFile(resourceDto);
        var resource = _mapper.Map<AzureResource>(resourceDto);
        _resourceRepository.Add(resource);
        return resource.Id;
    }

    public async Task<Guid> UpdateAsync(AzureResourceDto resourceDto, AzureResource resource)
    {
        resource.Name = resourceDto.Name;
        resource.Description = resourceDto.Description;

        if (resourceDto.File == null)
        {
            _resourceRepository.Update(resource);
            return resource.Id;
        }

        await UploadFile(resourceDto);

        resource.AzureFileName = resourceDto.AzureFileName!;
        resource.AzureStorageAbsolutePath = resourceDto.AzureStorageAbsolutePath!;

        _resourceRepository.Update(resource);

        return resource.Id;
    }
}