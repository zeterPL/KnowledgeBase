﻿using AutoMapper;
using Azure;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;

namespace KnowledgeBase.Logic.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    private readonly AzureStorageService _azureStorageService;

    public ResourceService(IResourceRepository resourceRepository, IMapper mapper,
        AzureStorageService azureStorageService, IProjectRepository projectRepository)
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

    private async Task<ResourceDto> UploadFile(ResourceDto resourceDto, string projectName)
    {
        if (resourceDto.File == null)
        {
            throw new ArgumentException("File cant be null");
        }

        var uploadFile = new UploadAzureResourceFile(resourceDto.Name, projectName, resourceDto.File);
        var azureResourceFile = await _azureStorageService.UploadFileAsync(uploadFile);

        resourceDto.AzureStorageAbsolutePath = azureResourceFile.AzureStoragePath;
        resourceDto.AzureFileName = azureResourceFile.AzureFileName;
        return resourceDto;
    }

    public async Task AddAsync(ResourceDto resourceDto)
    {
        var projectName = _projectRepository.Get(resourceDto.ProjectId)?.Name;
        if (projectName == null)
        {
            throw new ArgumentException("Project assigned to resource doesn't exist");
        }

        var createdResourceDto = await UploadFile(resourceDto, projectName);

        Resource resource = _mapper.Map<Resource>(createdResourceDto);
        _resourceRepository.Add(resource);
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

    public async Task UpdateAsync(ResourceDto resourceDto)
    {
        var id = resourceDto.Id.ToGuid();
        if (id == Guid.Empty)
        {
            return;
        }

        var resource = await _resourceRepository.GetResourceWithProjectAsync(id);
        if (resource == null)
        {
            return;
        }

        var newResource = _mapper.Map<Resource>(resourceDto);
        newResource.ProjectId = resource.ProjectId;
        foreach (var propertyInfo in resource.GetType().GetProperties())
        {
            if (propertyInfo.GetValue(newResource) != null)
            {
                propertyInfo.SetValue(resource, propertyInfo.GetValue(newResource));
            }
        }

        if (resourceDto.File == null)
        {
            _resourceRepository.Update(resource);
            return;
        }

        var uploadedResource = await UploadFile(resourceDto, resource.Project!.Name);

        resource.AzureFileName = uploadedResource.AzureFileName!;
        resource.AzureStorageAbsolutePath = uploadedResource.AzureStorageAbsolutePath!;

        _resourceRepository.Update(resource);
    }

    public IEnumerable<ResourceDto> GetAll()
    {
        IEnumerable<Resource> resourceList = _resourceRepository.GetAll().Where(r => !r.IsDeleted);
        return resourceList.Select(r => _mapper.Map<ResourceDto>(r));
    }

    public async Task<DownloadResourceDto?> DownloadAsync(Guid id)
    {
        var resource = Get(id);
        if (resource == null)
        {
            return null;
        }

        var fileDto = new AzureResourceFile
        {
            AzureStoragePath = resource.AzureStorageAbsolutePath,
        };

        try
        {
            var file = await _azureStorageService.DownloadFileAsync(fileDto);

            return new DownloadResourceDto(file.Stream, file.ContentType)
            {
                AzureFileName = resource.AzureFileName,
            };
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
}