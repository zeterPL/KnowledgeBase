using AutoMapper;
using Azure;
using FluentAssertions;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;

namespace KnowledgeBase.Tests.Services;

public class ResourceServiceTests
{
    private readonly Mock<IResourceRepository> _resourceRepository;
    private readonly Mock<IProjectRepository> _projectRepository;
    private readonly Mock<IAzureStorageService> _azureStorageService;
    private readonly Mock<IMapper> _mapper;
    private readonly IResourceService _resourceService;

    public ResourceServiceTests()
    {
        _resourceRepository = new Mock<IResourceRepository>();
        _projectRepository = new Mock<IProjectRepository>();
        _azureStorageService = new Mock<IAzureStorageService>();
        _mapper = new Mock<IMapper>();

        _resourceService = new ResourceService(_resourceRepository.Object, _mapper.Object,
            _azureStorageService.Object, _projectRepository.Object);
    }

    [Fact]
    public void Get_ResourceExistsInDatabase_ReturnsNotNull()
    {
        // arrange
        var resourceId = Guid.NewGuid();
        var resource = new Resource();
        var resourceDto = new ResourceDto();

        _resourceRepository.Setup(r => r.Get(resourceId)).Returns(resource);
        _mapper.Setup(m => m.Map<ResourceDto>(resource)).Returns(resourceDto);

        // act
        var result = _resourceService.Get(resourceId);

        // assert
        _resourceRepository.Verify(r => r.Get(resourceId), Times.Once);
        _mapper.Verify(m => m.Map<ResourceDto>(resource), Times.Once);
        result.Should().Be(resourceDto);
    }

    [Fact]
    public void Get_ResourceDoesntExistInDatabase_ReturnsNull()
    {
        // arrange
        var resourceId = Guid.NewGuid();

        _resourceRepository.Setup(r => r.Get(resourceId)).Returns((Resource?)null);
        _mapper.Setup(m => m.Map<ResourceDto>(null)).Returns((ResourceDto?)null);

        // act
        var result = _resourceService.Get(resourceId);

        // assert
        _resourceRepository.Verify(r => r.Get(resourceId), Times.Once);
        _mapper.Verify(m => m.Map<ResourceDto>(null), Times.Once);
        result.Should().BeNull();
    }

    [Fact]
    public void SoftDelete_InvalidGuid_Returns()
    {
        // arrange
        var resourceDto = new ResourceDto
        {
            Id = null,
        };

        // act
        _resourceService.SoftDelete(resourceDto);

        // assert
        _resourceRepository.Verify(r => r.Get(It.IsAny<Guid>()), Times.Never);
        _resourceRepository.Verify(r => r.SoftDelete(It.IsAny<Resource>()), Times.Never);
    }

    [Fact]
    public void SoftDelete_ResourceDoesntExistInDatabase_Returns()
    {
        // arrange
        var resourceId = Guid.NewGuid();
        var resourceDto = new ResourceDto
        {
            Id = resourceId,
        };

        _resourceRepository.Setup(r => r.Get(resourceId)).Returns((Resource?)null);

        // act
        _resourceService.SoftDelete(resourceDto);

        // assert
        _resourceRepository.Verify(r => r.Get(resourceId), Times.Once);
        _resourceRepository.Verify(r => r.SoftDelete(It.IsAny<Resource>()), Times.Never);
    }

    [Fact]
    public void SoftDelete_ResourceExistsInDatabase_CallsRepositorySoftDelete()
    {
        // arrange
        var resourceId = Guid.NewGuid();
        var resourceDto = new ResourceDto
        {
            Id = resourceId,
        };
        var resource = new Resource();

        _resourceRepository.Setup(r => r.Get(resourceId)).Returns(resource);

        // act
        _resourceService.SoftDelete(resourceDto);

        // assert
        _resourceRepository.Verify(r => r.Get(resourceId), Times.Once);
        _resourceRepository.Verify(r => r.SoftDelete(resource), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ProjectDoesntExistInDatabase_ThrowsArgumentException()
    {
        // arrange
        var projectId = Guid.NewGuid();
        var resourceDto = new ResourceDto
        {
            ProjectId = projectId,
        };

        _projectRepository.Setup(r => r.Get(projectId)).Returns((Project?)null);

        // act assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _resourceService.AddAsync(resourceDto));
        _projectRepository.Verify(r => r.Get(projectId), Times.Once);
    }

    [Fact]
    public async Task AddAsync_FileIsNull_ThrowsArgumentException()
    {
        // arrange
        var projectId = Guid.NewGuid();
        var project = new Project
        {
            Id = projectId,
            Name = "Project",
        };
        var resourceDto = new ResourceDto
        {
            ProjectId = projectId,
            File = null,
        };

        _projectRepository.Setup(r => r.Get(projectId)).Returns(project);

        // act assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _resourceService.AddAsync(resourceDto));
        _projectRepository.Verify(r => r.Get(projectId), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ValidResource_CallsAzureServiceUploadFile()
    {
        // arrange
        var file = new Mock<IFormFile>();
        var projectId = Guid.NewGuid();
        var project = new Project
        {
            Id = projectId,
            Name = "Project",
        };
        var resourceDto = new ResourceDto
        {
            ProjectId = projectId,
            File = file.Object,
        };

        var azureResource = new AzureResourceFile { AzureFileName = "FileName", AzureStoragePath = "AzurePath" };

        _projectRepository.Setup(r => r.Get(projectId)).Returns(project);
        _azureStorageService.Setup
            (s => s.UploadFileAsync(It.IsAny<UploadAzureResourceFile>()))
            .Returns(Task.Run(() => azureResource));

        // act
        await _resourceService.AddAsync(resourceDto);

        // assert
        _azureStorageService.Verify(s => s.UploadFileAsync(It.IsAny<UploadAzureResourceFile>()), Times.Once);
        _resourceRepository.Verify(s => s.Add(It.IsAny<Resource>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ResourceDoesntExistInDatabase_Returns()
    {
        var resourceId = Guid.NewGuid();
        var resourceDto = new ResourceDto
        {
            Id = resourceId,
        };

        _resourceRepository.Setup(r => r.GetResourceWithProjectAsync(resourceId)).Returns(async () => null);

        // act
        await _resourceService.UpdateAsync(resourceDto);

        // assert
        _resourceRepository.Verify(r => r.Update(It.IsAny<Resource>()), Times.Never);
        _azureStorageService.Verify(s => s.UploadFileAsync(It.IsAny<UploadAzureResourceFile>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_UpdatedResourceWithoutNewFile_UpdatesResourceInDatabase()
    {
        var projectId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var resourceWithProject = new Resource
        {
            Id = resourceId,
            ProjectId = projectId,
        };
        var resourceDto = new ResourceDto
        {
            Id = resourceId,
            File = null,
        };

        _resourceRepository.Setup
            (r => r.GetResourceWithProjectAsync(resourceId))
            .Returns(async () => resourceWithProject);
        _mapper.Setup(m => m.Map<Resource>(resourceDto)).Returns(new Resource());

        // act
        await _resourceService.UpdateAsync(resourceDto);

        // assert
        _resourceRepository.Verify(r => r.Update(It.IsAny<Resource>()), Times.Once);
        _azureStorageService.Verify(s => s.UploadFileAsync(It.IsAny<UploadAzureResourceFile>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_UpdatedResourceWithNewFile_UpdatesResourceInDatabaseAndUploadsFile()
    {
        var projectId = Guid.NewGuid();
        var project = new Project
        {
            Id = projectId,
            Name = "Project",
        };
        var resourceId = Guid.NewGuid();
        var resourceWithProject = new Resource
        {
            Id = resourceId,
            ProjectId = projectId,
            Project = project,
        };
        var file = new Mock<IFormFile>();
        var resourceDto = new ResourceDto
        {
            Id = resourceId,
            File = file.Object,
        };

        _resourceRepository.Setup
            (r => r.GetResourceWithProjectAsync(resourceId))
            .Returns(async () => resourceWithProject);
        _mapper.Setup(m => m.Map<Resource>(resourceDto)).Returns(new Resource());

        var azureResource = new AzureResourceFile { AzureFileName = "FileName", AzureStoragePath = "AzurePath" };
        _azureStorageService.Setup
            (s => s.UploadFileAsync(It.IsAny<UploadAzureResourceFile>()))
            .Returns(Task.Run(() => azureResource));

        // act
        await _resourceService.UpdateAsync(resourceDto);

        // assert
        _resourceRepository.Verify(r => r.Update(It.IsAny<Resource>()), Times.Once);
        _azureStorageService.Verify(s => s.UploadFileAsync(It.IsAny<UploadAzureResourceFile>()), Times.Once);
    }

    [Fact]
    public async Task DownloadAsync_ResourceDoesntExist_ReturnsNull()
    {
        var resourceId = Guid.NewGuid();
        _resourceRepository.Setup(r => r.Get(resourceId)).Returns((Resource?)null);

        // act
        var result = await _resourceService.DownloadAsync(resourceId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DownloadAsync_ValidResource_ReturnsResourceWithStream()
    {
        var resourceId = Guid.NewGuid();
        var resource = new Resource
        {
            AzureFileName = "FileName",
        };
        var resourceDto = new ResourceDto();
        var stream = new Mock<Stream>();
        var contentType = "content";

        _resourceRepository.Setup(r => r.Get(resourceId)).Returns(resource);
        _mapper.Setup(m => m.Map<ResourceDto>(resource)).Returns(resourceDto);

        _azureStorageService.Setup
            (s => s.DownloadFileAsync(It.IsAny<AzureResourceFile>()))
            .Returns(async () => new DownloadAzureResourceFile(stream.Object, contentType));

        // act
        DownloadResourceDto? result = await _resourceService.DownloadAsync(resourceId);

        // assert
        result.Should().NotBeNull();
        result.Content.Should().BeSameAs(stream.Object);
        result.ContentType.Should().BeSameAs(contentType);
        result.AzureFileName.Should().BeSameAs(result.AzureFileName);
    }

    public static IEnumerable<object[]> AzureExceptions()
    {
        yield return new object[] { new FileNotFoundException() };
        yield return new object[] { new RequestFailedException("") };
    }

    [Theory]
    [MemberData(nameof(AzureExceptions))]
    public async Task DownloadAsync_ValidResourceButAzureServiceThrows_ReturnsNull<TException>(TException exceptionType) where TException : Exception
    {
        var resourceId = Guid.NewGuid();
        var resource = new Resource();
        var resourceDto = new ResourceDto();

        _resourceRepository.Setup(r => r.Get(resourceId)).Returns(resource);
        _mapper.Setup(m => m.Map<ResourceDto>(resource)).Returns(resourceDto);
        _azureStorageService.Setup(s => s.DownloadFileAsync(It.IsAny<AzureResourceFile>())).Throws(exceptionType);

        // act
        var result = await _resourceService.DownloadAsync(resourceId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetAll_NoResourceExistsInDatabase_ReturnsEmptyEnumerable()
    {
        _resourceRepository.Setup(r => r.GetAll()).Returns(Array.Empty<Resource>());

        // act
        var result = _resourceService.GetAll();

        // assert
        result.ToArray().Should().BeEmpty();
    }

    [Fact]
    public void GetAll_NoDeletedResourcesExistInDatabse_ReturnsEveryResource()
    {
        var resources = new Resource[] { new Resource(), new Resource() };
        _resourceRepository.Setup(r => r.GetAll()).Returns(resources);

        // act
        var result = _resourceService.GetAll();

        // assert
        result.ToArray().Length.Should().Be(resources.Length);
    }

    [Fact]
    public void GetAll_DeletedResourcesExistInDatabse_ReturnsOnlyNotDeletedResources()
    {
        var resources = new Resource[] { new Resource { IsDeleted = false, } };
        var deletedResources = new Resource[] { new Resource { IsDeleted = true, } };
        var allResources = resources.Union(deletedResources);
        _resourceRepository.Setup(r => r.GetAll()).Returns(allResources);

        // act
        var result = _resourceService.GetAll();

        // assert
        result.ToArray().Length.Should().Be(resources.Length);
    }
}