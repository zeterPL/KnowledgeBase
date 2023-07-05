using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers;

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
		IEnumerable<Project> projects = projectService.GetAll();
		return View(projects.ToList());
	}

	[HttpGet]
	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(Project project)
	{
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
		Project project = projectService.Get(id);

		if (project == null)
		{
			return NotFound();
		}

		return View(project);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Edit(Project project)
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
		Project project = projectService.Get(id);
		projectService.SoftDelete(project);
		return RedirectToAction("List");
	}
}
