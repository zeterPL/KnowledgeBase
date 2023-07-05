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
            return View(_service.Get(id));
        }


        public IActionResult Delete(Guid id)
        {
            return View(_service.Get(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Resource resource)
        {
            _service.Deleted(_service.Get(resource.Id));
            return RedirectToAction(actionName: "Index");
        }
    }
}
