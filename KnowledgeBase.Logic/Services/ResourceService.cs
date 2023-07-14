using AutoMapper;
using Azure;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.Dto.Resources;
using KnowledgeBase.Logic.Dto.Resources.AzureResource;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;

namespace KnowledgeBase.Logic.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    private readonly IAzureStorageService _azureStorageService;

    public ResourceService(IResourceRepository resourceRepository, IMapper mapper,
        IAzureStorageService azureStorageService, IProjectRepository projectRepository)
    {
        _resourceRepository = resourceRepository;
        _mapper = mapper;
        _azureStorageService = azureStorageService;
        _projectRepository = projectRepository;
    }

    public ResourceDto? Get(Guid id)
    {
        var resource = _resourceRepository.Get(id);
        return _mapper.Map<ResourceDto>(resource);
    }

    private async Task<AzureResourceDto> UploadFile(AzureResourceDto resourceDto)
    {
        if (resourceDto.File == null)
        {
            throw new ArgumentException("File cant be null");
        }

        var uploadFile = new UploadAzureResourceFile(resourceDto.Name, resourceDto.ProjectId, resourceDto.File);
        var azureResourceFile = await _azureStorageService.UploadFileAsync(uploadFile);

        resourceDto.AzureStorageAbsolutePath = azureResourceFile.AzureStoragePath;
        resourceDto.AzureFileName = azureResourceFile.AzureFileName;
        return resourceDto;
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

    private async Task UpdateAzureResourceAsync(AzureResourceDto resourceDto, AzureResource resource)
    {
        resource.Name = resourceDto.Name;
        resource.Description = resourceDto.Description;

        if (resourceDto.File == null)
        {
            _resourceRepository.Update(resource);
            return;
        }

        var uploadedResource = await UploadFile(resourceDto);

        resource.AzureFileName = uploadedResource.AzureFileName!;
        resource.AzureStorageAbsolutePath = uploadedResource.AzureStorageAbsolutePath!;

        _resourceRepository.Update(resource);
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
            await UpdateAzureResourceAsync(azureDto, azureResource);
        }
    }

    public IEnumerable<ResourceDto> GetAll()
    {
        IEnumerable<Resource> resourceList = _resourceRepository.GetAll().Where(r => !r.IsDeleted);
        return resourceList.Select(r => _mapper.Map<ResourceDto>(r));
    }

    public async Task<DownloadResourceDto?> DownloadAsync(Guid id)
    {
        var resource = _resourceRepository.Get(id);
        if (resource == null || resource is not AzureResource azureResource)
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

    public AzureResourceDto? GetAzureResource(Guid id)
    {
        var resource = _resourceRepository.Get(id);
        if (resource is not AzureResource)
        {
            return null;
        }
        return _mapper.Map<AzureResourceDto>(resource);
    }

    private async Task AddAzureResource(CreateAzureResourceDto resourceDto)
    {
        await UploadFile(resourceDto);
        var resource = _mapper.Map<AzureResource>(resourceDto);
        _resourceRepository.Add(resource);
    }

    public async Task AddAsync<T>(T resourceDto) where T : ICreateResourceDto
    {
        if (!_projectRepository.ProjectExists(resourceDto.ProjectId))
        {
            throw new ArgumentException("Project assigned to resource doesn't exist");
        }

        if (resourceDto is CreateAzureResourceDto azureResourceDto)
        {
            await AddAzureResource(azureResourceDto);
        }
    }
}