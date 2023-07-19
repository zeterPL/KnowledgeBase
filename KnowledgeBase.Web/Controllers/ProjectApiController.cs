using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectApiController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectApiController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IEnumerable<ProjectDto> Get()
        {
            return _projectService.GetAll();
        }
        [HttpPost]
        public ActionResult Post([FromBody] ProjectDto dto)
        {
            return Ok(_projectService.Add(dto));
        }
    }
}
