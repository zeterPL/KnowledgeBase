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
using KnowledgeBase.Logic.Sorting;
using KnowledgeBase.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

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

    public ProjectService(IProjectRepository projectRepository,
        IUserProjectPermissionRepository permissionRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IMapper mapper,
        ITagRepository tagRepository,
        IProjectTagRepository projectTagsRepository, 
        IAzureServiceBusHandler serviceBusHandler,
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
			if (user.Id != userId)
				AddPermisionsToSpecificProject((Guid)projectId, user.Id);
		}
	}
  

    #endregion private methods

    #region public methods

    public Guid Add(ProjectDto projectDto)
    {
        var newProject = _mapper.Map<Project>(projectDto);
        newProject.StartDate = DateTime.Now;
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

    public IEnumerable<ProjectDto> GetNotOwned(Guid userId)
    {
        var projects = _projectRepository.GetAll().Where(p => !p.IsDeleted && p.OwnerId != userId);
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

	public IEnumerable<ProjectDto> GetAllReadableByUser(Guid userId, ProjectSortingTypes sortingType = ProjectSortingTypes.None)
	{
		if (sortingType == ProjectSortingTypes.NameAsc)
		{
			var projects = _projectRepository.GetAllReadableByUser(userId).OrderBy(x => x.Name);
			return projects.Select(p => _mapper.Map<ProjectDto>(p));
		}
		else if (sortingType == ProjectSortingTypes.NameDesc)
		{
			var projects = _projectRepository.GetAllReadableByUser(userId).OrderByDescending(x => x.Name);
			return projects.Select(p => _mapper.Map<ProjectDto>(p));
		}
		else if (sortingType == ProjectSortingTypes.DateAsc)
		{
			var projects = _projectRepository.GetAllReadableByUser(userId).OrderBy(x => x.StartDate);
			return projects.Select(p => _mapper.Map<ProjectDto>(p));
		}
		else if (sortingType == ProjectSortingTypes.DateDesc)
		{
			var projects = _projectRepository.GetAllReadableByUser(userId).OrderByDescending(x => x.StartDate);
		}

		return _projectRepository.GetAllReadableByUser(userId)
			.Select(p => _mapper.Map<ProjectDto>(p));
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

    public IEnumerable<ProjectDto>? FindProjects(string? query, List<Guid>? tagsId, DateTime? dateFrom,
        DateTime? dateTo, Guid userId)
    {
        var projects = _projectRepository.GetAllReadableByUser(userId);

        if (!tagsId.IsNullOrEmpty())
        {
            var projectTags = _projectTagRepository.GetAll().Where(x => tagsId.Contains(x.TagId))
                .Select(x => x.ProjectId);

			projects = projects.Where(x => projectTags.Contains(x.Id));
		}

        if (dateFrom.HasValue)
        {
            projects = projects.Where(x => x.StartDate >= dateFrom);
        }

        if (dateTo.HasValue)
        {
            projects = projects.Where(x => x.StartDate <= dateTo);
        }

        if (!query.IsNullOrEmpty())
        {
            List<ProjectDto> findproject = _tagRepository.GetAll()
                .Where(tag => tag.Name.Equals(query))
                .Select(tag => tag.Id)
                .SelectMany(tagId => _projectTagRepository.GetByTagtId(tagId))
                .Join(projects, projectJoin => projectJoin.ProjectId, userProject => userProject.Id,
                    (projectJoin, userProject) => Get(projectJoin.ProjectId))
                .ToList();

			projects = projects.Where(x => x.Name.Contains(query) || x.Description.Contains(query) || findproject.Any(fp => x.Id.Equals(fp.Id)));
		}       

        return projects.Select(p => _mapper.Map<ProjectDto>(p)).ToList();
    }

    public List<SelectListItem> GetAllTagsAsSelectItems(Guid userId)
    {
        var projects = _projectRepository.GetAllReadableByUser(userId).Select(x => x.Id);
        var tags = _projectTagRepository.GetAll().Where(tag => projects.Contains(tag.ProjectId)).Select(x => x.TagId)
            .ToList();
        var tagsList = _tagRepository.GetAll().Where(tag => tags.Contains(tag.Id))
            .Select(z => new SelectListItem { Text = z.Name, Value = z.Id.ToString() })
            .ToList();
        return tagsList;
    }


    public async Task RequestPermissionsAsync(RequestPermissionDto requestPermissionDto)
    {
        var project = _projectRepository.GetProjectWithOwner(requestPermissionDto.ProjectId);
        if (project is null)
        {
            throw new Exception("Project doesn't exist");
        }

        if (project.OwnerId == requestPermissionDto.SenderId)
        {
            throw new Exception("Can't request permissions to your own project");
        }

        var sender = _userRepository.Get(requestPermissionDto.SenderId);
        if (sender is null)
        {
            throw new Exception("Sender doesn't exist");
        }

        var requests = requestPermissionDto.Permissions.Select(
            permission => new ProjectPermissionRequest
            {
                SenderId = requestPermissionDto.SenderId,
                ReceiverId = project.OwnerId,
                ProjectId = requestPermissionDto.ProjectId,
                RequestedPermission = permission,
                TimeRequested = DateTime.Now,
            }).ToList();

        // Remove requests which already exist in database
        var existingRequests =
            _permissionRequestRepository.ProjectPermissionRequestsExists(
                requests.Select(r => r.RequestedPermission),
                requestPermissionDto.ProjectId,
                requestPermissionDto.SenderId);

        requests.RemoveAll(request =>
            existingRequests.Any(existingRequest =>
                existingRequest.RequestedPermission == request.RequestedPermission));

        await _permissionRequestRepository.AddRangeAsync(requests);

        // Send message to service bus
        var requestDto = new ProjectPermissionsRequestDto(
            $"{sender.FirstName} {sender.LastName}",
            project.Owner.Email,
            project.Name,
            requestPermissionDto.Permissions);

        await _serviceBusHandler.SendMessageAsync(requestDto.ToJson());
    }

    public IEnumerable<ProjectPermissionName> GetAvailableUserProjectPermissions(Guid projectId, Guid userId)
    {
        var userProjectPermissions = _permissionRepository.GetAll()
            .Where(p => p.UserId == userId)
            .Select(p => p.PermissionName);
        var availablePermissions = Enum.GetValues<ProjectPermissionName>().ToList();
        availablePermissions.RemoveAll(p => userProjectPermissions.Contains(p));

        return availablePermissions;
    }

    #endregion public methods
}