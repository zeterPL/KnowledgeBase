using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KnowledgeBase.Web.Controllers;

[Authorize]
public class ProjectController : Controller
{
	private readonly IProjectService projectService;
	public ProjectController(IProjectService projectService)
	{
		this.projectService = projectService;
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult List()
	{
		IEnumerable<ProjectDto> projects = projectService.GetAll();
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
		var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

		project.User = new UserDto
		{
			Id = userId,
		};

		if (!ModelState.IsValid)
		{
			// return View(project);
		}

		projectService.Add(project);
		return RedirectToAction("List");
	}

	[HttpGet]
	public IActionResult Edit(Guid id)
	{
		ProjectDto? project = projectService.Get(id);

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
		if (!ModelState.IsValid)
		{
			return View(project);
		}

		projectService.Update(project);
		return RedirectToAction("List");
	}

	[HttpGet]
	public IActionResult Delete(Guid id)
	{
		projectService.SoftDelete(new ProjectDto{Id = id});
		return RedirectToAction("List");
	}

	[HttpGet]
	public IActionResult Details(Guid id)
	{
		ProjectDto? project = projectService.Get(id);
		if(project == null)
		{
			return NotFound();
		}

		return View(project);
	}
}
