using AutoMapper;
using Azure;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.Dto.Resources;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;
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
    private readonly ResourceHandlersManager _resourceHandlers;

    public ResourceService(IResourceRepository resourceRepository, IMapper mapper,
        IProjectRepository projectRepository,
        IAzureStorageService azureStorageService, ResourceHandlersManager resourceHandlers)
    {
        _resourceRepository = resourceRepository;
        _mapper = mapper;
        _projectRepository = projectRepository;
        _azureStorageService = azureStorageService;
        _resourceHandlers = resourceHandlers;
    }

    public T? Get<T>(Guid id) where T : ResourceDto
    {
        var resource = _resourceRepository.Get(id);
        return _mapper.Map<T>(resource);
    }

    public void SoftDelete(IResourceDto resourceDto)
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

    public IEnumerable<IResourceDto> GetAll()
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

    public async Task UpdateAsync<T>(T resourceDto) where T : IUpdateResourceDto
    {
        var id = resourceDto.Id;
        if (id == Guid.Empty)
        {
            return;
        }

        var resource = await _resourceRepository.GetResourceWithProjectAsync(id);
        if (resource == null)
        {
            return;
        }

        resourceDto.ProjectId = resource.ProjectId;

        await _resourceHandlers.GetResourceHandler<T>().UpdateAsync(resourceDto);
    }

    public async Task AddAsync<T>(T resourceDto) where T : ICreateResourceDto
    {
        if (!_projectRepository.ProjectExists(resourceDto.ProjectId))
        {
            throw new ArgumentException("Project assigned to resource doesn't exist");
        }

        await _resourceHandlers.GetResourceHandler<T>().AddAsync(resourceDto);
    }
}