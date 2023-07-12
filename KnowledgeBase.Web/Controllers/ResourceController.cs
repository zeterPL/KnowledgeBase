using Azure;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly IProjectService _projectService;

        public ResourceController(IResourceService resourceService, IProjectService projectService)
        {
            _resourceService = resourceService;
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            return View(_resourceService.GetAll().ToList());
        }

        private CreateResourceDto SetUpAssignableProjects(CreateResourceDto dto)
        {
            var projects = _projectService.GetAllReadableByUser(dto.UserId);
            dto.AssignableProjects = projects;
            return dto;
        }

        [Authorize]
        public IActionResult Create()
        {
            var resourceDto = SetUpAssignableProjects(new CreateResourceDto { UserId = User.GetUserId() });
            return View(resourceDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateResourceDto resourceDto)
        {
            resourceDto.UserId = User.GetUserId();

            if (!ModelState.IsValid)
            {
                return View(SetUpAssignableProjects(resourceDto));
            }

            try
            {
                resourceDto.File = resourceDto.NewFile;
                await _resourceService.AddAsync(resourceDto);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (RequestFailedException ex)
            {
                return StatusCode(ex.Status);
            }

            return RedirectToAction(actionName: "Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ResourceDto resourceDto)
        {
            if (!ModelState.IsValid)
            {
                return View(resourceDto);
            }

            resourceDto.UserId = User.GetUserId();

            try
            {
                await _resourceService.UpdateAsync(resourceDto);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (RequestFailedException ex)
            {
                return StatusCode(ex.Status);
            }

            return RedirectToAction(actionName: "Index");
        }

        public IActionResult Edit(Guid id)
        {
            var resource = _resourceService.Get(id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        public IActionResult Delete(Guid id)
        {
            return View(_resourceService.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ResourceDto resourceDto)
        {
            _resourceService.SoftDelete(resourceDto);
            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        public async Task<IActionResult> Download(Guid id)
        {
            var resource = await _resourceService.DownloadAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            return File(resource.Content, resource.ContentType, resource.AzureFileName);
        }
    }
}