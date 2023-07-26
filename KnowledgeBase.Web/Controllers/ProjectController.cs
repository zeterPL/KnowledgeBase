using CsvHelper;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Dto.Project;
using KnowledgeBase.Logic.Exceptions;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;
using KnowledgeBase.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    public readonly ILogger<ProjectController> _logger;
    private readonly ITagService _tagService;
    private readonly IUserService _userService;
    private readonly IProjectInterestedUserService _projectInterestedUserService;

    public ProjectController(IProjectService projectService, ILogger<ProjectController> logger,
        ITagService tagService, IUserService userService, IProjectInterestedUserService projectInterestedUserService)
    {
        _projectService = projectService;
        _logger = logger;
        _tagService = tagService;
        _userService = userService;
        _projectInterestedUserService = projectInterestedUserService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult List()
    {
        try
        {
            _logger.LogInformation("getting all projects");
            IEnumerable<ProjectDto> projects = _projectService.GetAllReadableByUser(User.GetUserId());
            return View(projects.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest("internal server error");
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        var project = new ProjectDto { StartDate = DateTime.Now };
        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProjectDto project)
    {
        try
        {
            _logger.LogInformation("creating project");
            var userId = User.GetUserId();

            project.UserId = userId;

            ModelState.Clear();
            TryValidateModel(project);
            if (!ModelState.IsValid)
            {
                return View(project);
            }

            _projectService.Add(project);
            return RedirectToAction("List");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest("Create internal server error");
        }
    }

    [HttpGet]
    [Authorize(Policy = ProjectPermission.CanEditProject)]
    public IActionResult Edit(Guid id)
    {
        try
        {
            _logger.LogInformation("Editin project");
            ProjectDto? project = _projectService.Get(id);
            return View(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return NotFound("Can't find project");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = ProjectPermission.CanEditProject)]
    public IActionResult Edit(ProjectDto project)
    {
        project.UserId = Guid.NewGuid();
        ModelState.Clear();
        TryValidateModel(project);
        if (!ModelState.IsValid)
        {
            return View(project);
        }

        _projectService.UpdateWithoutUserId(project);
        return RedirectToAction("List");
    }

    [HttpGet]
    [Authorize(Policy = ProjectPermission.CanDeleteProject)]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _logger.LogInformation("Delete project");
            _projectService.SoftDelete(new ProjectDto { Id = id });
            return RedirectToAction("List");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return NotFound("Can't delete project");
        }
    }

    [HttpGet]
    [Authorize(Policy = ProjectPermission.CanReadProject)]
    public IActionResult Details(Guid id)
    {
        try
        {
            _logger.LogInformation("Detailing project");
            ProjectDto? project = _projectService.Get(id);
            return View(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return NotFound("Project is null");
        }
    }

    [HttpGet]
    [Authorize(Policy = ProjectPermission.CanReadProject)]
    public IActionResult ManageTags(Guid id)
    {
        var tags = _projectService.GetAllTagsByProjectId(id);
        ViewBag.projectId = id;
        return View(tags);
    }

    [HttpGet]
    [Authorize(Policy = ProjectPermission.CanEditProject)]
    public IActionResult AddTags(Guid id)
    {
        ViewBag.ProjectId = id;
        return View();
    }

    [HttpPost]
    [Authorize(Policy = ProjectPermission.CanEditProject)]
    public IActionResult AddTags(AddTagDto addTagDto, Guid id)
    {
        addTagDto.ProjectId = id;
        //addTagDto.ProjectId = ViewBag.ProjectId;
        if (ModelState.IsValid)
        {
            string[] tagsNames = addTagDto.Tags.Split(',');

            foreach (string tagName in tagsNames)
            {
                TagDto tag = _tagService.GetTagByName(tagName.Trim()) ?? new TagDto { Name = tagName.Trim() };

                if (tag.Id == Guid.Empty)
                {
                    var NewId = _tagService.Add(tag);
                    tag.Id = NewId;
                }

                _projectService.AddTagToProject(tag, addTagDto.ProjectId);
            }

            return RedirectToAction("ManageTags", new { id = addTagDto.ProjectId });
        }

        return View(addTagDto);
    }

    [HttpGet]
    [Authorize(Policy = ProjectPermission.CanEditProject)]
    public IActionResult DeleteTag(Guid TagId, Guid ProjectId)
    {
        TagDto tag = new TagDto { Id = TagId };
        _projectService.RemoveTagFromProject(tag, ProjectId);
        return RedirectToAction("ManageTags", new { id = ProjectId });
    }

    [HttpGet]
    public IActionResult AssignUsers(Guid id)
    {
        var users = _userService.GetUsersNotInterestedInProject(id);
        var addedUsers = _userService.GetInterestedUsersByProjectId(id);
        ViewBag.Users = users;
        ViewBag.AddedUsers = addedUsers;
        ViewBag.ProjectId = id;
        var selectedUsers = new List<Guid>();
        return View(selectedUsers);
    }

    [HttpPost]
    public IActionResult AssignUsers(List<Guid> selectedUsers, Guid id)
    {
        _projectInterestedUserService.AddInterestedUsersToSpecificProjectByUsersIds(selectedUsers, id);
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult EditInterestedUser(Guid userId, Guid projectId)
    {
        var interested = _projectInterestedUserService.GetInterestedUserByUserIdAndProjectId(userId, projectId);
        ViewBag.UserId = interested.UserId;
        ViewBag.ProjectId = interested.ProjectId;
        ViewBag.Id = interested.Id;
        return View(interested);
    }

    [HttpPost]
    public IActionResult EditInterestedUser(ProjectInterestedUserDto interested)
    {
        if (ModelState.IsValid)
        {
            _projectInterestedUserService.Update(interested);
            return RedirectToAction("AssignUsers", new { id = interested.ProjectId });
        }

        return View(interested);
    }

    [HttpGet]
    public IActionResult DeleteInterestedUser(Guid userId, Guid projectId)
    {
        var interested = _projectInterestedUserService.GetInterestedUserByUserIdAndProjectId(userId, projectId);

        _projectInterestedUserService.Delete(interested);

        return RedirectToAction("AssignUsers", new { id = projectId });
    }

    [HttpGet]
    public IActionResult CreateProjectsFromCsv()
    {
        return View(new CreateProjectsFromFileDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectsFromCsv(CreateProjectsFromFileDto dto)
    {
        try
        {
            await _projectService.AddProjectsFromFileAsync(dto, User.GetUserId());
        }
        catch (ProjectsExistsInDatabaseException e)
        {
            return View(new CreateProjectsFromFileDto
            {
                ExistingProjects = e.Projects
            });
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return RedirectToAction("List");
    }

    public IActionResult Find()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult FoundProject(ProjectDto project)
    {
        try
        {
            _logger.LogInformation("Project Found");
            var projects = _projectService.ProjectSearchFilter(project, User.GetUserId());
            return View(projects);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return NotFound("there is no project");
        }
    }

    public IActionResult FindByTag()
    {
        return View();
    }
}