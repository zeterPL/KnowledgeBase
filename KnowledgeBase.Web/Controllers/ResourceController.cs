using Azure;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using KnowledgeBase.Shared;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public IActionResult Index()
        {
            return View(_resourceService.GetAll().ToList());
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

            try
            {
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
            resourceDto.ProjectId = Guid.Parse("8f94efce-fa7a-47d8-98e6-08db7ede4d7b");

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