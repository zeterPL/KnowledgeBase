using AutoMapper;
using Azure;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.Dto.Resources;
using KnowledgeBase.Logic.Dto.Resources.AzureResource;
using KnowledgeBase.Logic.ResourceHandlers;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;

namespace KnowledgeBase.Logic.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    private readonly IAzureStorageService _azureStorageService;
    private readonly IResourceHandler<AzureResource, AzureResourceDto, CreateAzureResourceDto> _azureResourceHandler;

    public ResourceService(IResourceRepository resourceRepository, IMapper mapper,
        IProjectRepository projectRepository,
        IResourceHandler<AzureResource, AzureResourceDto, CreateAzureResourceDto> azureResourceHandler,
        IAzureStorageService azureStorageService)
    {
        _resourceRepository = resourceRepository;
        _mapper = mapper;
        _projectRepository = projectRepository;
        _azureResourceHandler = azureResourceHandler;
        _azureStorageService = azureStorageService;
    }

    public T Get<T>(Guid id) where T : ResourceDto?
    {
        var resource = _resourceRepository.Get(id);
        return _mapper.Map<T>(resource);
    }

    public void SoftDelete(ResourceDto resourceDto)
    {
        var id = resourceDto.Id.ToGuid();
        if (id == Guid.Empty)
        {
            return;
        }

        var resource = _resourceRepository.Get(id);
        if (resource == null)
        {
            return;
        }

        _resourceRepository.SoftDelete(resource);
    }

    public IEnumerable<ResourceDto> GetAll()
    {
        IEnumerable<Resource> resourceList = _resourceRepository.GetAll().Where(r => !r.IsDeleted);
        return resourceList.Select(r => _mapper.Map<ResourceDto>(r));
    }

    public async Task<DownloadResourceDto?> DownloadAsync(Guid id)
    {
        var resource = _resourceRepository.Get(id);
        if (resource is not AzureResource azureResource)
        {
            return null;
        }

        var fileDto = new AzureResourceFile
        {
            AzureStoragePath = azureResource.AzureStorageAbsolutePath,
        };

        try
        {
            var file = await _azureStorageService.DownloadFileAsync(fileDto);

            return new DownloadResourceDto(file.Stream, file.ContentType, azureResource.AzureFileName);
        }
        catch (FileNotFoundException)
        {
            return null;
        }
        catch (RequestFailedException)
        {
            return null;
        }
    }

    public async Task UpdateAsync<T>(T resourceDto) where T : ResourceDto
    {
        var id = resourceDto.Id.ToGuid();
        if (id == Guid.Empty)
        {
            return;
        }

        var resource = await _resourceRepository.GetResourceWithProjectAsync(id);
        if (resource == null || resource.Category != resourceDto.Category)
        {
            return;
        }

        resourceDto.ProjectId = resource.ProjectId;

        if (resourceDto is AzureResourceDto azureDto && resource is AzureResource azureResource)
        {
            await _azureResourceHandler.UpdateAsync(azureDto, azureResource);
        }
    }

    public async Task AddAsync<T>(T resourceDto) where T : ICreateResourceDto
    {
        if (!_projectRepository.ProjectExists(resourceDto.ProjectId))
        {
            throw new ArgumentException("Project assigned to resource doesn't exist");
        }

        if (resourceDto is CreateAzureResourceDto azureResourceDto)
        {
            await _azureResourceHandler.AddAsync(azureResourceDto);
        }
    }
}