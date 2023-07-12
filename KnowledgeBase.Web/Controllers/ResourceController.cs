using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Logic.Dto;
using System.Security.Claims;
using KnowledgeBase.Logic.AzureServices;
using KnowledgeBase.Logic.AzureServices.File;
using KnowledgeBase.Shared;
using Azure;

namespace KnowledgeBase.Web.Controllers
{
	public class ResourceController : Controller
	{
		private readonly IResourceService _service;
		private readonly AzureStorageService _azureStorageService;
		private readonly IProjectService _projectService;

		public ResourceController(IResourceService service, AzureStorageService azureStorageService, IProjectService projectService)
		{
			_service = service;
			_azureStorageService = azureStorageService;
			_projectService = projectService;
		}

		public IActionResult Index()
		{
			return View(_service.GetAll().ToList());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ResourceDto resourceDto)
		{
			resourceDto.UserId = User.GetUserId();
			resourceDto.ProjectId = Guid.Parse("8f94efce-fa7a-47d8-98e6-08db7ede4d7b");
			var projectName = _projectService.Get(resourceDto.ProjectId)?.Name;
			if (projectName == null)
			{
				return BadRequest();
			}

			try
			{
				var uploadFile = new UploadAzureResourceFile(resourceDto.Name, projectName, resourceDto.File);
				var azureResourceFile = await _azureStorageService.UploadFileAsync(uploadFile);

				resourceDto.AzureStorageAbsolutePath = azureResourceFile.AzureStoragePath;
				resourceDto.AzureFileName = azureResourceFile.AzureFileName;
				_service.Add(resourceDto);
			}
			catch (RequestFailedException ex)
			{
				return StatusCode(ex.Status);
			}

			return RedirectToAction(actionName: "Index");
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ResourceDto resourcedto)
		{
			if (!ModelState.IsValid)
			{
				return View(resourcedto);
			}
			resourcedto.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			resourcedto.ProjectId = Guid.Parse("8f94efce-fa7a-47d8-98e6-08db7ede4d7b");
			_service.Update(resourcedto);
			return RedirectToAction(actionName: "Index");
		}


		public IActionResult Edit(Guid id)
		{
			return View(_service.Get(id));
		}


		public IActionResult Delete(Guid id)
		{
			return View(_service.Get(id));
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(ResourceDto resourcedto)
		{
			_service.Delete(resourcedto);
			return RedirectToAction(actionName: "Index");
		}

		[HttpGet]
		public async Task<IActionResult> Download(Guid id)
		{
			var resource = _service.Get(id);
			var fileDto = new AzureResourceFile
			{
				AzureStoragePath = resource.AzureStorageAbsolutePath,
			};

			try
			{
				var file = await _azureStorageService.DownloadFileAsync(fileDto);
				return File(file.Stream, file.ContentType, resource.AzureFileName);
			}
			catch(FileNotFoundException)
			{
				return NotFound();
			}
			catch (RequestFailedException ex)
			{
				return StatusCode(ex.Status);
			}
		}
	}
}
