using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;

namespace KnowledgeBase.Logic.Services;

public class ProjectService : IProjectService
{
	private readonly IMapper _mapper;
	private readonly IProjectRepository _projectRepository;
	private readonly IUserProjectPermissionRepository _permissionRepository;

	public ProjectService(IProjectRepository projectRepository, IUserProjectPermissionRepository permissionRepository, IMapper mapper)
    {
        this._projectRepository = projectRepository;
        this._permissionRepository = permissionRepository;
        this._mapper = mapper;
    }

	private void SavePermissions(IEnumerable<UserProjectPermission> permissions)
	{
		foreach (var permission in permissions)
		{
			_permissionRepository.Add(permission);
		}
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
	public Guid Add(ProjectDto projectDto)
    {
        var newProject = _mapper.Map<Project>(projectDto);
		_projectRepository.Add(newProject);

		// Default permissions
		var permissions = DefaultCreatePermissions.Select(p => new UserProjectPermission
		{
			PermissionName = p,
			UserId = projectDto.UserId.ToGuid(),
			ProjectId = newProject.Id,
		});
		SavePermissions(permissions);

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
        return projects.Select(p => _mapper.Map<ProjectDto>(p));
    }

    public Guid Update(ProjectDto projectDto)
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
}