using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace KnowledgeBase.Web.Controllers;

public class ProjectController : Controller
{
	private readonly IProjectService projectService;
	public readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();
	public ProjectController(IProjectService projectService)
	{
		this.projectService = projectService;
	}

	public IActionResult Index()
	{
		try
		{
			Logger.Debug("Hi I am NLog Debug Level");
			Logger.Info("Hi I am NLog Info Level");
			Logger.Warn("Hi I am NLog Warn Level");
			throw new NullReferenceException();
			return View();
		}
		catch (Exception ex)
		{
			Logger.Error(ex, "Hi I am NLog Error Level");
			Logger.Fatal(ex, "Hi I am NLog Fatal Level");
			throw;
		}
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
		if (!ModelState.IsValid)
		{
			return View(project);
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
