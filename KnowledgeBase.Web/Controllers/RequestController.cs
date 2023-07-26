using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Dto.PermissionsRequests;
using KnowledgeBase.Logic.Services;
using KnowledgeBase.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers;

[Authorize]
public class RequestController : Controller
{
    private readonly PermissionsRequestsService _permissionsRequestsService;

    public RequestController(PermissionsRequestsService permissionsRequestsService)
    {
        _permissionsRequestsService = permissionsRequestsService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ProjectPermissionsRequests()
    {
        var userId = User.GetUserId();
        var requests = _permissionsRequestsService.GetProjectPermissionsRequestsSendToUser(userId);
        return View(requests);
    }
}