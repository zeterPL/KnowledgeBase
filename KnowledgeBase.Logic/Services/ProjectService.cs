using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.AzureServices.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Dto.PermissionsRequests;
using KnowledgeBase.Logic.Dto.Project;
using KnowledgeBase.Logic.Exceptions;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;

namespace KnowledgeBase.Logic.Services;

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserProjectPermissionRepository _permissionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IProjectTagRepository _projectTagRepository;
    private readonly IAzureServiceBusHandler _serviceBusHandler;
    private readonly IPermissionRequestRepository _permissionRequestRepository;

    public ProjectService(IProjectRepository projectRepository, IUserProjectPermissionRepository permissionRepository,
        IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, ITagRepository tagRepository,
        IProjectTagRepository projectTagsRepository, IAzureServiceBusHandler serviceBusHandler,
        IPermissionRequestRepository permissionRequestRepository)
    {
        _projectRepository = projectRepository;
        _permissionRepository = permissionRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _tagRepository = tagRepository;
        _projectTagRepository = projectTagsRepository;
        _serviceBusHandler = serviceBusHandler;
        _permissionRequestRepository = permissionRequestRepository;
    }

    #region private methods

    private void SavePermissions(IEnumerable<UserProjectPermission> permissions)
    {
        _permissionRepository.AddRange(permissions);
    }

    private static ICollection<ProjectPermissionName> DefaultCreatePermissions
    {
        get => new List<ProjectPermissionName>
        {
            ProjectPermissionName.ReadProject,
            ProjectPermissionName.EditProject,
            ProjectPermissionName.DeleteProject,
        };
    }

    private void AddPermisionsToSpecificProject(Guid projectId, Guid userId)
    {
        var user = _userRepository.Get(userId);
        var role = _roleRepository.Get(user.RoleId);
        var roleName = role.Name;
        List<UserProjectPermission> permissions = new List<UserProjectPermission>();
        if (roleName == UserRoles.SuperAdmin.ToString())
        {
            UserProjectPermission perm = new UserProjectPermission
            {
                UserId = userId,
                ProjectId = projectId,
                PermissionName = ProjectPermissionName.ReadProject
            };
            permissions.Add(perm);

            UserProjectPermission perm1 = new UserProjectPermission
            {
                UserId = userId,
                ProjectId = projectId,
                PermissionName = ProjectPermissionName.EditProject
            };
            permissions.Add(perm1);

            UserProjectPermission perm2 = new UserProjectPermission
            {
                UserId = userId,
                ProjectId = projectId,
                PermissionName = ProjectPermissionName.DeleteProject
            };
            permissions.Add(perm2);
        }

        if (roleName == UserRoles.Admin.ToString())
        {
            UserProjectPermission perm = new UserProjectPermission
            {
                UserId = userId,
                ProjectId = projectId,
                PermissionName = ProjectPermissionName.ReadProject
            };
            permissions.Add(perm);

            UserProjectPermission perm1 = new UserProjectPermission
            {
                UserId = userId,
                ProjectId = projectId,
                PermissionName = ProjectPermissionName.EditProject
            };
            permissions.Add(perm1);
        }

        _permissionRepository.AddRange(permissions);
    }

    private void AssignPermissionsToSuperUsers(Guid? projectId, Guid userId)
    {
        var allUsers = _userRepository.GetAll();
        foreach (var user in allUsers)
        {
            AddPermisionsToSpecificProject((Guid)projectId, userId);
        }
    }

    #endregion private methods

    #region public methods

    public Guid Add(ProjectDto projectDto)
    {
        var newProject = _mapper.Map<Project>(projectDto);
        var newProjectId = _projectRepository.Add(newProject);

        // Default permissions
        var permissions = DefaultCreatePermissions.Select(p => new UserProjectPermission
        {
            PermissionName = p,
            UserId = projectDto.OwnerId.ToGuid(),
            ProjectId = newProject.Id,
        });
        SavePermissions(permissions);

        AssignPermissionsToSuperUsers(newProjectId, projectDto.OwnerId.ToGuid());

        return newProject.Id;
    }

    public ProjectDto? Get(Guid id)
    {
        var project = _projectRepository.Get(id);
        return _mapper.Map<ProjectDto>(project);
    }

    public IEnumerable<ProjectDto> GetAll()
    {
        var projects = _projectRepository.GetAll().Where(p => !p.IsDeleted);
        var dtos = projects.Select(p => _mapper.Map<ProjectDto>(p));
        foreach (var dto in dtos)
        {
            var user = _userRepository.Get(dto.OwnerId.ToGuid());
            dto.Owner = user.ToUserDto();
        }

        return dtos;
    }

    public Guid UpdateWithoutUserId(ProjectDto projectDto)
    {
        var id = projectDto.Id.ToGuid();
        if (id == Guid.Empty)
        {
            return Guid.Empty;
        }

        if (!_projectRepository.ProjectExists(id))
        {
            return Guid.Empty;
        }

        var newProject = _mapper.Map<Project>(projectDto);

        _projectRepository.Update(newProject);
        return id;
    }

    public void SoftDelete(ProjectDto projectDto)
    {
        var id = projectDto.Id.ToGuid();
        if (id == Guid.Empty)
        {
            return;
        }

        var project = _projectRepository.Get(id);
        if (project == null) // Project doesnt exist
        {
            return;
        }

        _projectRepository.SoftDelete(project);
    }

    public IEnumerable<ProjectDto> GetAllReadableByUser(Guid userId)
    {
        var projects = _projectRepository.GetAllReadableByUser(userId);
        return projects.Select(p => _mapper.Map<ProjectDto>(p));
    }

    public IList<TagDto> GetAllTagsByProjectId(Guid projectId)
    {
        return _tagRepository.GetAllByProjectId(projectId).Select(t => t.ToTagDto()).ToList();
    }

    public void AddTagToProject(TagDto tagDto, Guid projectId)
    {
        ProjectTag projectTag = new ProjectTag
        {
            ProjectId = projectId,
            TagId = tagDto.Id,
        };
        _projectTagRepository.Add(projectTag);
    }

    public void RemoveTagFromProject(TagDto tagDto, Guid projectId)
    {
        var projectTags = _projectTagRepository.GetByProjectId(projectId);
        ProjectTag tagToRemove = projectTags.Where(pt => pt.TagId == tagDto.Id).FirstOrDefault();

        _projectTagRepository.RemoveByTagAndProjectId(tagDto.Id, projectId);
    }

    public async Task<IEnumerable<Guid>> AddProjectsFromFileAsync(CreateProjectsFromFileDto dto, Guid userId)
    {
        using var stream = new MemoryStream();
        await dto.File.CopyToAsync(stream);
        stream.Position = 0;

        IFileReader reader = new CsvFileReader();
        var projectsDtos = reader.ReadProjects(stream).ToList();

        var existingProjects = _projectRepository.ProjectsExists(projectsDtos.Select(p => p.Name)).ToList();

        if (existingProjects.Any())
        {
            var dtos = existingProjects.Select(p => new ProjectDto
            {
                Name = p.Name,
                Description = p.Description,
                StartDate = p.StartDate,
            });
            throw new ProjectsExistsInDatabaseException(dtos);
        }

        var projects = projectsDtos.Select(p =>
            new Project
            {
                Name = p.Name,
                Description = p.Description,
                StartDate = p.StartDate,
                IsDeleted = false,
                OwnerId = userId,
            }).ToList();
        await _projectRepository.AddRangeAsync(projects);

        var permissions = projects.SelectMany(_ => DefaultCreatePermissions,
            (project, permission) =>
                new UserProjectPermission
                {
                    PermissionName = permission,
                    UserId = userId,
                    ProjectId = project.Id,
                });

        await _permissionRepository.AddRangeAsync(permissions);

        return projects.Select(p => p.Id);
    }

    public async Task RequestPermissionsAsync(RequestPermissionDto requestPermissionDto)
    {
        var ownerId = await _projectRepository.GetProjectOwnerId(requestPermissionDto.ProjectId);
        var requestDto = new ProjectPermissionsRequestDto(
            requestPermissionDto.SenderId,
            ownerId,
            requestPermissionDto.ProjectId,
            requestPermissionDto.Permissions);

        await _serviceBusHandler.SendMessageAsync(requestDto.ToJson());
        var requests = requestPermissionDto.Permissions.Select(
            permission => new ProjectPermissionRequest
            {
                SenderId = requestPermissionDto.SenderId,
                ReceiverId = ownerId,
                ProjectId = requestPermissionDto.ProjectId,
                RequestedPermission = permission,
                TimeRequested = DateTime.Now,
            });

        await _permissionRequestRepository.AddRangeAsync(requests);
    }

    #endregion public methods
}