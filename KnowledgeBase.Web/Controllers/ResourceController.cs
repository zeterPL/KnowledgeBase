using KnowledgeBase.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Logic.Services.Interfaces;

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
			IEnumerable<Resource> resources = _service.GetAll();
			return View(resources.ToList());
		}

		public IActionResult Create()
		{
			return View();
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Resource resource)
		{
			_service.Add(resource);
			return RedirectToAction(actionName: "Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Resource resource)
		{
			if (!ModelState.IsValid)
			{
				return View(resource);
			}

			_service.Update(resource);
			return RedirectToAction(actionName: "Index");
		}

		public IActionResult Edit(Guid id)
		{
			Resource resource = _service.Get(id);

			if (resource == null)
			{
				return NotFound();
			}
			return View(resource);
		}


		
		public IActionResult Delete(Guid id)
		{
			Resource resource = _service.Get(id);

			if (resource == null)
			{
				return NotFound();
			}
			return View(resource);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Resource resource)
		{
			_service.Remove(resource);
			return RedirectToAction(actionName: "Index");
		}
	}
}
