using KnowledgeBase.Logic.Dto;
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

    public ProjectController(IProjectService projectService, ILogger<ProjectController> logger, ITagService tagService)
    {
        _projectService = projectService;
        _logger = logger;
        _tagService = tagService;
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
        return View();
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
            var projects = _projectService.GetAllReadableByUser(User.GetUserId()).Where(p => p.Name.Contains(project.Name));
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult FoundProjectByTag(TagDto tagDto)
    {
        try
        {
            _logger.LogInformation("Project Found");
            var redableByUserAndFiltered = _projectService.GetAllProjectsByTagName(tagDto, User.GetUserId());
            return View(redableByUserAndFiltered);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return NotFound("there is no project with this tagName");
        }
    }

    public IActionResult FindByDate()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult FoundProjectByDate(DateTime startDate, DateTime endDate)
    {
        try
        {
            if (startDate.Date > endDate.Date && (startDate.Date != DateTime.MinValue && endDate.Date != DateTime.MinValue))
            {
                throw new Exception("Invalid Input vaues(start Date > end Date)");
            }
            var redableByUserAndFiltered = _projectService.GetAllProjectsByDate(startDate.Date, endDate.Date, User.GetUserId());
            return View(redableByUserAndFiltered);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return NotFound(ex.Message);
        }
    }



}