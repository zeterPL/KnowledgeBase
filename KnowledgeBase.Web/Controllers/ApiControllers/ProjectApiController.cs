using Humanizer;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Dto.Project;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectApiController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public readonly ILogger<ProjectController> _logger;
        private readonly ITagService _tagService;

        public ProjectApiController(IProjectService projectService, ITagService tagService)
        {
            _projectService = projectService;
            _tagService = tagService;
        }

        [HttpGet]
        public IEnumerable<ProjectDto> Get()
        {
            return _projectService.GetAll();
        }

        [Route("ProjectAPI/projectId={id}")]
        [HttpGet]
        public IActionResult Get([FromQuery] string id)
        {
            try
            {
                return Ok(_projectService.Get(Guid.Parse(id)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("there is no project with this id");
            }
        }

        [Route("ProjectAPI/OwnerId={id}")]
        [HttpGet]
        public IActionResult GetByUser([FromQuery] string userId)
        {
            try
            {
                return Ok(_projectService.GetAllReadableByUser(Guid.Parse(userId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("there is no project with this id");
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody] ProjectDto dto)
        {
            try
            {
                return Ok(_projectService.Add(dto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Create API internal server error");
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProjectDto dto)
        {
            try
            {
                return Ok(_projectService.UpdateWithoutUserId(dto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Updating API internal server error");
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] ProjectDto dto)
        {
            try
            {
                _projectService.SoftDelete(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Delete API internal server error");
            }

        }
    }
}
