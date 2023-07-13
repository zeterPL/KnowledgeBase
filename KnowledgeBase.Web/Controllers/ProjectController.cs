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
    private readonly ITagService _tagService;

    public ProjectController(IProjectService projectService, ITagService tagService)
    {
        _projectService = projectService;
        _tagService = tagService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult List()
    {
        IEnumerable<ProjectDto> projects = _projectService.GetAllReadableByUser(User.GetUserId());
        return View(projects.ToList());
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
        var userId = User.GetUserId();
        if (userId == Guid.Empty)
        {
            return Forbid();
        }

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

    [HttpGet]
    [Authorize(Policy = ProjectPermission.CanEditProject)]
    public IActionResult Edit(Guid id)
    {
        ProjectDto? project = _projectService.Get(id);

        if (project == null)
        {
            return NotFound();
        }

        return View(project);
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
        _projectService.SoftDelete(new ProjectDto { Id = id });
        return RedirectToAction("List");
    }

    [HttpGet]
    [Authorize(Policy = ProjectPermission.CanReadProject)]
    public IActionResult Details(Guid id)
    {
        ProjectDto? project = _projectService.Get(id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
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
   // [Authorize(Policy = ProjectPermission.CanEditProject)]
    public IActionResult AddTags(Guid id)
    {      
        ViewBag.ProjectId = id;
        return View();
    }

    [HttpPost]
    //[Authorize(Policy = ProjectPermission.CanEditProject)]
    public IActionResult AddTags(AddTagDto addTagDto, Guid id)
    {
        addTagDto.ProjectId = id;
        //addTagDto.ProjectId = ViewBag.ProjectId;
        if(ModelState.IsValid)
        {
            string[] tagsNames = addTagDto.Tags.Split(',');

            foreach (string tagName in tagsNames)
            {
                TagDto tag = _tagService.GetTagByName(tagName.Trim()) ?? new TagDto { Name = tagName.Trim() };

                if(tag.Id == Guid.Empty)
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

}