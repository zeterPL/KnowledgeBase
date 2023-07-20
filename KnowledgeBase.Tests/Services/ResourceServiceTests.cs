using AutoMapper;
using Azure;
using FluentAssertions;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Logic.Dto.Resources;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;
using KnowledgeBase.Logic.ResourceHandlers;
using KnowledgeBase.Logic.Services;
using KnowledgeBase.Logic.Services.Interfaces;
using Moq;

namespace KnowledgeBase.Tests.Services;

public class ResourceServiceTests
{
    private readonly Mock<IResourceRepository> _resourceRepository;
    private readonly Mock<IProjectRepository> _projectRepository;
    private readonly Mock<IAzureStorageService> _azureStorageService;
    private readonly Mock<IUserResourcePermissionRepository> _permissionRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ResourceHandlersManager> _resourceHandlerManager;
    private readonly Mock<IUserResourcePermissionRepository> _permissionRepository;
    private readonly IResourceService _resourceService;

    public ResourceServiceTests()
    {
        _resourceRepository = new Mock<IResourceRepository>();
        _projectRepository = new Mock<IProjectRepository>();
        _azureStorageService = new Mock<IAzureStorageService>();
        _permissionRepository = new Mock<IUserResourcePermissionRepository>();
        _mapper = new Mock<IMapper>();
        _resourceHandlerManager = new Mock<ResourceHandlersManager>(new List<IResourceHandler>());
        _permissionRepository = new Mock<IUserResourcePermissionRepository>();

        _resourceService = new ResourceService(_resourceRepository.Object, _mapper.Object,
            _azureStorageService.Object, _projectRepository.Object, _permissionRepository.Object);
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
        var result = _resourceService.Get<ResourceDto>(resourceId);

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
        var result = _resourceService.Get<ResourceDto>(resourceId);

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
        var createResourceDto = new Mock<ICreateResourceDto>();
        createResourceDto.Setup(r => r.ProjectId).Returns(projectId);

        _projectRepository.Setup(r => r.ProjectExists(projectId)).Returns(false);

        // act assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _resourceService.AddAsync(createResourceDto.Object));
        _projectRepository.Verify(r => r.ProjectExists(projectId), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ResourceDoesntExistInDatabase_Returns()
    {
        var resourceId = Guid.NewGuid();
        var updateResourceDto = new Mock<IUpdateResourceDto>();
        updateResourceDto.Setup(r => r.Id).Returns(resourceId);

        _resourceRepository.Setup(r => r.GetResourceWithProjectAsync(resourceId)).Returns(async () => null);

        // act
        await _resourceService.UpdateAsync(updateResourceDto.Object);

        // assert
        _resourceRepository.Verify(r => r.Update(It.IsAny<Resource>()), Times.Never);
        _azureStorageService.Verify(s => s.UploadFileAsync(It.IsAny<UploadAzureResourceFile>()), Times.Never);
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
        var resource = new AzureResource
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
        result.FileName.Should().BeSameAs(resource.AzureFileName);
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