using SDSDemo.Interfaces;
using SDSDemo.Models;
using System.Web.Mvc;

namespace SDSDemo.Controllers
{
    public class RecyclableTypeController : Controller
    {
        private readonly IRecyclableTypeRepository _recyclableTypeRepository;

        public RecyclableTypeController(IRecyclableTypeRepository recyclableTypeRepository)
        {
            _recyclableTypeRepository = recyclableTypeRepository;
        }

        public ActionResult Index()
        {
            var recyclableTypes = _recyclableTypeRepository.GetAll();
            return View(recyclableTypes);
        }

        public ActionResult Details(int id)
        {
            var recyclableType = _recyclableTypeRepository.GetById(id);
            if (recyclableType == null)
                return HttpNotFound();

            return View(recyclableType);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecyclableType recyclableType)
        {
            // Custom validation
            if (recyclableType.MinKg <= 0 || recyclableType.MaxKg <= 0)
            {
                ModelState.AddModelError("MinKg", "MinKg and MaxKg must be greater than zero.");
                ModelState.AddModelError("MaxKg", "MinKg and MaxKg must be greater than zero.");
            }
            if (recyclableType.MinKg >= recyclableType.MaxKg)
            {
                ModelState.AddModelError("MaxKg", "MaxKg must be greater than MinKg.");
            }
            if (ModelState.IsValid)
            {
                _recyclableTypeRepository.Add(recyclableType);
                return RedirectToAction("Index");
            }

            return View(recyclableType);
        }

        public ActionResult Edit(int id)
        {
            var recyclableType = _recyclableTypeRepository.GetById(id);
            if (recyclableType == null)
                return HttpNotFound();

            return View(recyclableType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecyclableType recyclableType)
        {
            // Custom validation
            if (recyclableType.MinKg <= 0 || recyclableType.MaxKg <= 0)
            {
                ModelState.AddModelError("MinKg", "MinKg and MaxKg must be greater than zero.");
                ModelState.AddModelError("MaxKg", "MinKg and MaxKg must be greater than zero.");
            }
            if (recyclableType.MinKg >= recyclableType.MaxKg)
            {
                ModelState.AddModelError("MaxKg", "MaxKg must be greater than MinKg.");
            }
            if (ModelState.IsValid)
            {
                _recyclableTypeRepository.Update(recyclableType);
                return RedirectToAction("Index");
            }

            return View(recyclableType);
        }

        public ActionResult Delete(int id)
        {
            var recyclableType = _recyclableTypeRepository.GetById(id);
            if (recyclableType == null)
                return HttpNotFound();

            return View(recyclableType);
        }

        [HttpGet]
        public ActionResult GetRateByTypeId(int id)
        {
            var recyclableType = _recyclableTypeRepository.GetById(id);
            if (recyclableType != null)
            {
                return Json(recyclableType.Rate, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _recyclableTypeRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
