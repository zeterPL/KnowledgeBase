using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
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

    public ProjectService(IProjectRepository projectRepository, IUserProjectPermissionRepository permissionRepository,
        IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, ITagRepository tagRepository,
        IProjectTagRepository projectTagsRepository)
    {
        _projectRepository = projectRepository;
        _permissionRepository = permissionRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _tagRepository = tagRepository;
        _projectTagRepository = projectTagsRepository;
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
        newProject.CreationDate = DateTime.Now;
        var newProjectId = _projectRepository.Add(newProject);

        // Default permissions
        var permissions = DefaultCreatePermissions.Select(p => new UserProjectPermission
        {
            PermissionName = p,
            UserId = projectDto.UserId.ToGuid(),
            ProjectId = newProject.Id,
        });
        SavePermissions(permissions);

        AssignPermissionsToSuperUsers(newProjectId, projectDto.UserId.ToGuid());

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

    public IEnumerable<ProjectDto> GetAllProjectsByTagName(TagDto tagDto, Guid userId)
    {
        string[] tagsSplitFromInput;

        if (tagDto.Name.Contains(", "))
        {
            tagsSplitFromInput = tagDto.Name.Split(", ");
        }
        else
        {
            tagsSplitFromInput = new string[] { tagDto.Name };
        }

        var tags = _tagRepository.GetAll()
            .Where(tag => tagsSplitFromInput.Contains(tag.Name))
            .ToList();

        var tagsId = tags.Select(tag => tag.Id).ToList();

        var projects = tagsId.SelectMany(t => _projectTagRepository.GetByTagtId(t)).ToList();

        var allRedableByUser = _projectRepository.GetAllReadableByUser(userId);

        var findproject = projects
            .Where(project => allRedableByUser.Any(userProject => userProject.Id == project.ProjectId))
            .Select(project => Get(project.ProjectId))
            .ToList();
        return findproject.Select(p => _mapper.Map<ProjectDto>(p));
    }

    public IEnumerable<ProjectDto> GetAllProjectsByDate(DateTime startDate, DateTime endDate, Guid userId)
    {
        var projects = _projectRepository.GetAllReadableByUser(userId);

        var filteredProjects = projects.Where(project =>
            (startDate == DateTime.MinValue || project.CreationDate >= startDate) &&
            (endDate == DateTime.MinValue || project.CreationDate <= endDate)
        );

        return filteredProjects.Select(p => _mapper.Map<ProjectDto>(p));
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

    #endregion public methods
}