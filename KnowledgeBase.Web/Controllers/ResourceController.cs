using Humanizer.Localisation;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Web.Controllers
{
    public class ResourceController : Controller
    {

        private readonly IGenericRepository<Resource> _service;
        public ResourceController(IGenericRepository<Resource> service)
        {
            _service = service;
        }


        // GET: ResourceController
        public ActionResult Index(Guid id)
        {
            var result = _service.Get(id);
            return View(result);
        }



        // POST: ResourceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Resource resource)
        {
            _service.Add(resource);
            return View();
        }

        [HttpPut]
        // Put: ResourceController/Edit/5
        public ActionResult Edit(Resource resource)
        {
            _service.Update(resource);
            return View();
        }


        // Delete: ResourceController/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Resource resource)
        {
            _service.Remove(resource);
            return View();
        }
    }
}
