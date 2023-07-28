using AutoMapper;
using Azure;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.AzureServices.Interfaces;
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
    private readonly IUserResourcePermissionRepository _permissionRepository;
    private readonly IMapper _mapper;
    private readonly IAzureStorageService _azureStorageService;
    private readonly ResourceHandlersManager _resourceHandlers;

    public ResourceService(IResourceRepository resourceRepository,
        IMapper mapper,
        IProjectRepository projectRepository,
        IAzureStorageService azureStorageService,
        ResourceHandlersManager resourceHandlers,
        IUserResourcePermissionRepository permissionRepository)
    {
        _resourceRepository = resourceRepository;
        _mapper = mapper;
        _projectRepository = projectRepository;
        _azureStorageService = azureStorageService;
        _resourceHandlers = resourceHandlers;
        _permissionRepository = permissionRepository;
    }

    public T? Get<T>(Guid id) where T : ResourceDto
    {
        var resource = _resourceRepository.Get(id);
        return _mapper.Map<T>(resource);
    }

    #region private methods

    private void AddDefaultPermissions(Guid userId, Guid resourceId)
    {
        var list = new List<UserResourcePermission>();
        UserResourcePermission permission1 = new UserResourcePermission
        {
            UserId = userId,
            ResourceId = resourceId,
            Name = Data.Models.Enums.ResourcePermissionName.CanSave
        };
        list.Add(permission1);
        UserResourcePermission permission2 = new UserResourcePermission
        {
            UserId = userId,
            ResourceId = resourceId,
            Name = Data.Models.Enums.ResourcePermissionName.CanRead
        };
        list.Add(permission2);
        UserResourcePermission permission3 = new UserResourcePermission
        {
            UserId = userId,
            ResourceId = resourceId,
            Name = Data.Models.Enums.ResourcePermissionName.CanEdit
        };
        list.Add(permission3);
        UserResourcePermission permission4 = new UserResourcePermission
        {
            UserId = userId,
            ResourceId = resourceId,
            Name = Data.Models.Enums.ResourcePermissionName.CanDelete
        };
        list.Add(permission4);
        UserResourcePermission permission5 = new UserResourcePermission
        {
            UserId = userId,
            ResourceId = resourceId,
            Name = Data.Models.Enums.ResourcePermissionName.CanDownload
        };
        list.Add(permission5);
        _permissionRepository.AddRange(list);
    }

    #endregion private methods

    #region public methods

    public ResourceDto? Get(Guid id)
    {
        var resource = _resourceRepository.Get(id);
        return _mapper.Map<ResourceDto>(resource);
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

        resource.Name = resourceDto.Name;
        resource.Description = resourceDto.Description;
        resourceDto.ProjectId = resource.ProjectId;

        resource = await _resourceHandlers.UpdateDetails(resourceDto, resource);
        _resourceRepository.Update(resource);
    }

    public async Task AddAsync<T>(T resourceDto) where T : ICreateResourceDto
    {
        if (!_projectRepository.ProjectExists(resourceDto.ProjectId))
        {
            throw new ArgumentException("Project assigned to resource doesn't exist");
        }

        var resource = new Resource
        {
            Name = resourceDto.Name,
            Description = resourceDto.Description,
            Category = resourceDto.Category,
            ProjectId = resourceDto.ProjectId,
            UserId = resourceDto.UserId,
            IsDeleted = false,
        };

        var newResource = await _resourceHandlers.UpdateDetails(resourceDto, resource);
        _resourceRepository.Add(newResource);
        AddDefaultPermissions(newResource.UserId, newResource.Id);
    }

    #endregion public methods
}