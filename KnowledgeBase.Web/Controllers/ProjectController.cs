using CsvHelper;
using KnowledgeBase.Data.Models.Enums;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Dto.Project;
using KnowledgeBase.Logic.Exceptions;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Logic.Sorting;
using KnowledgeBase.Logic.ViewModels;
using KnowledgeBase.Shared;
using KnowledgeBase.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KnowledgeBase.Web.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    public readonly ILogger<ProjectController> _logger;
    private readonly ITagService _tagService;
    private readonly IUserService _userService;
    private readonly IProjectInterestedUserService _projectInterestedUserService;
    private readonly IReportProjectIssueService _reportService;
    private readonly IPermissionService _permissionService;

    public ProjectController(IProjectService projectService,
        ILogger<ProjectController> logger,
        ITagService tagService, IUserService userService,
        IProjectInterestedUserService projectInterestedUserService,
        IReportProjectIssueService reportService,
        IPermissionService permissionService)
    {
        _projectService = projectService;
        _logger = logger;
        _tagService = tagService;
        _userService = userService;
        _projectInterestedUserService = projectInterestedUserService;
        _reportService = reportService;
        _permissionService = permissionService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public IActionResult ListAll()
    {
        var projects = _projectService.GetNotOwned(User.GetUserId());
        return View(projects);
    }

    public IActionResult List(ProjectSortingTypes sortingType = ProjectSortingTypes.None)
    {
        try
        {
            _logger.LogInformation($"getting all projects sorted by {sortingType.ToString()}");
            IEnumerable<ProjectDto> projects = _projectService.GetAllReadableByUser(User.GetUserId(), sortingType);
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

            project.OwnerId = userId;

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
        project.OwnerId = Guid.NewGuid();
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
    public IActionResult ProjectsArchive()
    {
        var projects = _projectService.GetArchivedProjects();
        return View(projects);
    }

    [HttpGet]
    public IActionResult HardDelete(Guid id)
    {
        var project = _projectService.Get(id);

        if (project is null)
            return NotFound();
        else
            return View(project);
    }

    [HttpPost]
    public IActionResult HardDelete(ProjectDto project)
    {
        _projectService.Delete(project);
        return RedirectToAction("ProjectsArchive");
    }

    [HttpGet]
    [Authorize(Policy = ProjectPermission.CanReadProject)]
    public IActionResult Details(Guid id)
    {
        try
        {
            _logger.LogInformation("Detailing project");
            ProjectDto? project = _projectService.Get(id);
            var openedIssuesCount = _reportService.GetOpenedByProjectId(id).Count;
            ViewBag.Count = openedIssuesCount;
            var tags = _tagService.GetAllByProjectId(id);
            ViewBag.Tags = tags;    

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

    public IActionResult FindProjects()
    {
        ViewBag.ItemsToSelect = _projectService.GetAllTagsAsSelectItems(User.GetUserId());
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult FindProject(ProjectSearchFilter project)
    {
        try
        {
            _logger.LogInformation("Project Found");
            var projects = _projectService.FindProjects(project.Name, project.TagsId, project.DateFrom, project.DateTo,
                User.GetUserId());
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


    [HttpGet]
    public IActionResult AddReport(Guid id)
    {
        var project = _projectService.Get(id);
        if (project is null)
            return NotFound();

        ReportProjectIssueDto report = new ReportProjectIssueDto();
        ViewBag.ProjectName = project.Name;
        ViewBag.ProjectId = id;

        return View(report);
    }

    [HttpPost]
    public IActionResult AddReport(ReportProjectIssueDto report, Guid id)
    {
        report.Id = Guid.Empty;
        var userId = User.GetUserId();
        report.UserId = userId;
        report.ProjectId = id;

        var type = Request.Form["type"];
        if (type == "Help") report.IssueType = ReportProjectIssuesTypes.Help;
        else if (type == "BugReport") report.IssueType = ReportProjectIssuesTypes.BugReport;
        else if (type == "Info") report.IssueType = ReportProjectIssuesTypes.Info;

        report.IsOpen = true;
        report.CreatedDate = DateTime.Now;

        if (ModelState.IsValid)
        {
            _reportService.Create(report);
            return RedirectToAction("ReportsList", new { id = report.ProjectId });
        }

        return View(report);
    }

    [HttpGet]
    public IActionResult ReportsList(Guid id)
    {
        var reports = _reportService.GetOpenedByProjectId(id);
        if (reports is null) return NotFound();
        ViewBag.ProjectId = id;

        return View(reports);
    }

    [HttpGet]
    public IActionResult ArchiveReports(Guid id)
    {
        var archiveReports = _reportService.GetClosedByProjectId(id);
        if (archiveReports is null) return NotFound();

        return View(archiveReports);
    }

    [HttpGet]
    public IActionResult ReportDetails(Guid id)
    {
        var report = _reportService.Get(id);

        if (report is null)
            return NotFound();
        var user = _userService.GetById(report.UserId);
        if (user is null)
            return NotFound();

        ViewBag.UserFirstName = user.FirstName;
        ViewBag.UserLastName = user.LastName;
        ViewBag.UserId = user.Id;

        return View(report);
    }

    [HttpGet]
    public IActionResult CloseReport(Guid id)
    {
        var report = _reportService.Get(id);
        if (report is null)
            return NotFound();
        else
            _reportService.Close(id);

        return RedirectToAction("ReportsList", new { id = report.ProjectId });
    }

    [HttpGet]
    public IActionResult ReopenReport(Guid id)
    {
        var report = _reportService.Get(id);
        if (report is null)
            return NotFound();
        else
            _reportService.ReOpen(id);

        return RedirectToAction("ReportsList", new { id = report.ProjectId });
    }

    [HttpGet]
    public IActionResult DeleteReport(Guid id)
    {
        var report = _reportService.Get(id);
        if (report is null) return NotFound();
        _reportService.Delete(id);

        return RedirectToAction("ArchiveReports", new { id = report.ProjectId });
    }

    [HttpGet]
    [Authorize]
    [Route("Project/RequestPermission/{projectId:guid}")]
    public IActionResult RequestPermission(Guid projectId)
    {
        var availablePermissions = _projectService.GetAvailableUserProjectPermissions(projectId, User.GetUserId());

        return View(new RequestPermissionDto
        {
            AvailablePermissions = availablePermissions,
        });
    }

    [HttpPost]
    [Authorize]
    [Route("Project/RequestPermission/{projectId:guid}")]
    public async Task<IActionResult> RequestPermission(Guid projectId, RequestPermissionDto requestPermissionDto)
    {
        if (requestPermissionDto.Permissions == null)
        {
            return RequestPermission(projectId);
        }

        requestPermissionDto.ProjectId = projectId;
        requestPermissionDto.SenderId = User.GetUserId();
        await _projectService.RequestPermissionsAsync(requestPermissionDto);
        
        return RedirectToAction("ListAll");
    }
}