using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers;

public class ProjectController : Controller
{
	private readonly IProjectService projectService;
	public readonly ILogger<ProjectController> _logger;

	public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
	{
		this.projectService = projectService;
		_logger = logger;
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
			IEnumerable<ProjectDto> projects = projectService.GetAll();
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
			if (!ModelState.IsValid)
			{
				return View(project);
			}

			projectService.Add(project);
			return RedirectToAction("List");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return BadRequest("Create internal server error");
		}
	}

	[HttpGet]
	public IActionResult Edit(Guid id)
	{
		try
		{
			_logger.LogInformation("Editin project");
			ProjectDto? project = projectService.Get(id);
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
		try
		{
			_logger.LogInformation("Delete project");
			projectService.SoftDelete(new ProjectDto { Id = id });
			return RedirectToAction("List");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return NotFound("Can't delete project");
		}

	}

	[HttpGet]
	public IActionResult Details(Guid id)
	{
		try
		{
			_logger.LogInformation("Detailing project");
			ProjectDto? project = projectService.Get(id);
			return View(project);
		}
		catch(Exception ex)
		{
			_logger.LogError(ex.Message);
			return NotFound("Project is null");
		}
	}
}