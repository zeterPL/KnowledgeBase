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

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
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

        project.User = new UserDto
        {
            Id = userId,
        };

        if (!ModelState.IsValid)
        {
            // TODO validation
            // return View(project);
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
        if (!ModelState.IsValid)
        {
            // TODO validation
            // return View(project);
        }

        _projectService.Update(project);
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
}