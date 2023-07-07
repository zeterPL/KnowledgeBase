using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Data.Models.Enums;

namespace KnowledgeBase.Web.Controllers;

[Authorize]
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IPermissionService _permissionService;

    public ProjectController(IProjectService projectService, IPermissionService permissionService)
    {
        _projectService = projectService;
        _permissionService = permissionService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult List()
    {
        IEnumerable<ProjectDto> projects = _projectService.GetAll();
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
    public IActionResult Edit(Guid id)
    {
        var userId = User.GetUserId();
        if (userId == Guid.Empty ||
            !_permissionService.UserHadProjectPermission(userId, id, PermissionName.EditProject))
        {
            return Forbid();
        }

        ProjectDto? project = _projectService.Get(id);

        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProjectDto project)
    {
        var userId = User.GetUserId();
        if (userId == Guid.Empty ||
            !_permissionService.UserHadProjectPermission(userId, project.Id.ToGuid(), PermissionName.EditProject))
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            // TODO validation
            // return View(project);
        }

        _projectService.Update(project);
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Delete(Guid id)
    {
        var userId = User.GetUserId();
        if (userId == Guid.Empty ||
            !_permissionService.UserHadProjectPermission(userId, id, PermissionName.DeleteProject))
        {
            return Forbid();
        }

        _projectService.SoftDelete(new ProjectDto { Id = id });
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Details(Guid id)
    {
        var userId = User.GetUserId();
        if (userId == Guid.Empty ||
            !_permissionService.UserHadProjectPermission(userId, id, PermissionName.ReadProject))
        {
            return Forbid();
        }

        ProjectDto? project = _projectService.Get(id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }
}