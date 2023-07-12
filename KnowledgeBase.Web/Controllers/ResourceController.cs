using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KnowledgeBase.Web.Controllers
{
	public class ResourceController : Controller
	{
		private readonly IResourceService _service;
		private readonly UserManager<User> _userManager;
		public readonly ILogger<ProjectController> _logger;

		public ResourceController(IResourceService service, UserManager<User> userManager, ILogger<ProjectController> logger)
		{
			_service = service;
			_userManager = userManager;
			_logger = logger;
		}

		public IActionResult Index()
		{
			try
			{
				_logger.LogInformation("getting all resources");
				return View(_service.GetAll().ToList());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return BadRequest("internal server error");
			}
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ResourceDto resourcedto)
		{
			try
			{
				_logger.LogInformation("creating resource");

				resourcedto.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
				resourcedto.ProjectId = Guid.Parse("8f94efce-fa7a-47d8-98e6-08db7ede4d7b");
				_service.Add(resourcedto);
				return RedirectToAction(actionName: "Index");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return BadRequest("Create internal server error");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ResourceDto resourcedto)
		{
			try
			{
				_logger.LogInformation("editing resource");

				resourcedto.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
				resourcedto.ProjectId = Guid.Parse("8f94efce-fa7a-47d8-98e6-08db7ede4d7b");
				_service.Update(resourcedto);
				return RedirectToAction(actionName: "Index");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return NotFound("Can't find project");
			}
		}

		public IActionResult Edit(Guid id)
		{
			try
			{
				_logger.LogInformation("editing resource");

				return View(_service.Get(id));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return NotFound("Can't find project");
			}
		}

		public IActionResult Delete(Guid id)
		{
			try
			{
				_logger.LogInformation("deleting resource");

				return View(_service.Get(id));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return NotFound("Can't delete project");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(ResourceDto resourcedto)
		{
			try
			{
				_logger.LogInformation("deleting resource");

				_service.Delete(resourcedto);
				return RedirectToAction(actionName: "Index");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return NotFound("Project is null");
			}
		}
	}
}