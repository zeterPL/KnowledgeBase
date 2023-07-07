using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KnowledgeBase.Web.Controllers
{
	public class ResourceController : Controller
	{
		private readonly IResourceService _service;
		private readonly UserManager<User> _userManager;
		public ResourceController(IResourceService service, UserManager<User> userManager)
		{
			_service = service;
			_userManager = userManager;
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
		public IActionResult Create(ResourceDto resourcedto)
		{
			resourcedto.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			_service.Add(resourcedto);
			return RedirectToAction(actionName: "Index");
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ResourceDto resourcedto)
		{
			if (!ModelState.IsValid)
			{
				// return View(resource);
			}
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
			_service.Deleted(resourcedto);
			return RedirectToAction(actionName: "Index");
		}
	}
}
