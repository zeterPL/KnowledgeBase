using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceApiController : ControllerBase
    {
        private readonly IResourceService _resourceService;
        private readonly IProjectService _projectService;

        public ResourceApiController(IResourceService resourceService, IProjectService projectService)
        {
            _resourceService = resourceService;
            _projectService = projectService;
        }

        [Route("/ResourceController")]
        [HttpGet]
        public IEnumerable<ResourceDto> Get()
        {
            return _resourceService.GetAll();
        }

        [HttpPost]
        public IActionResult Create([FromBody] ResourceDto resourcedto)
        {
            return Ok(_resourceService.AddAsync(resourcedto));
        }

        [HttpPut]
        public IActionResult Put([FromBody] ResourceDto resourcedto)
        {
            return Ok(_resourceService.UpdateAsync(resourcedto));
        }

        [HttpDelete]
        [HttpPut]
        public IActionResult Delete([FromBody] ResourceDto resourcedto)
        {
            _resourceService.SoftDelete(resourcedto);
            return NoContent();
        }
    }
}
