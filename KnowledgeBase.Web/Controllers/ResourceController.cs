using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.AutoMapper;

namespace KnowledgeBase.Web.Controllers
{
	public class ResourceController : Controller
	{
		private readonly IResourceService _service;
		public ResourceController(IResourceService service)
		{
			_service = service;
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
