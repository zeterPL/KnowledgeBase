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
	public IActionResult Create(Project project)
	{
		projectService.Add(project);
		return View();
	}
}
