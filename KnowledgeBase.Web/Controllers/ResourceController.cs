using Azure;
using KnowledgeBase.Logic.Dto.Resources;
using KnowledgeBase.Logic.Dto.Resources.AzureResource;
using KnowledgeBase.Logic.Dto.Resources.Interfaces;
using KnowledgeBase.Logic.Dto.Resources.NoteResource;
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

        private ICreateResourceDto SetUpAssignableProjects(ICreateResourceDto dto)
        {
            var projects = _projectService.GetAllReadableByUser(dto.UserId);
            dto.AssignableProjects = projects;
            return dto;
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateAzure()
        {
            var resourceDto = SetUpAssignableProjects(new CreateAzureResourceDto { UserId = User.GetUserId() });
            return View((CreateAzureResourceDto)resourceDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAzure(CreateAzureResourceDto resourceDto)
        {
            resourceDto.UserId = User.GetUserId();

            if (!ModelState.IsValid)
            {
                return View((CreateAzureResourceDto)SetUpAssignableProjects(resourceDto));
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

        [HttpGet]
        public IActionResult EditAzure(Guid id)
        {
            var resource = _resourceService.Get<ResourceDto>(id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(new UpdateAzureResourceDto { Name = resource.Name, Description = resource.Description });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAzure(UpdateAzureResourceDto resourceDto)
        {
            if (!ModelState.IsValid)
            {
                return View(resourceDto);
            }

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

        public IActionResult Delete(Guid id)
        {
            return View(_resourceService.Get<ResourceDto>(id));
        }

        [HttpGet]
        public IActionResult CreateNote()
        {
            var resourceDto = SetUpAssignableProjects(new CreateNoteResourceDto { UserId = User.GetUserId() });
            return View((CreateNoteResourceDto)resourceDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(CreateNoteResourceDto resourceDto)
        {
            resourceDto.UserId = User.GetUserId();
            resourceDto.Category = Data.Models.Enums.ResourceCategory.Note;

            if (!ModelState.IsValid)
            {
                return View((CreateNoteResourceDto)SetUpAssignableProjects(resourceDto));
            }

            await _resourceService.AddAsync(resourceDto);

            return RedirectToAction(actionName: "Index");
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

            return File(resource.Content, resource.ContentType, resource.FileName);
        }
    }
}