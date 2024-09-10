using SDSDemo.Interfaces;
using SDSDemo.Models;
using System.Web.Mvc;

namespace SDSDemo.Controllers
{
    public class RecyclableItemController : Controller
    {
        private readonly IRecyclableItemRepository _recyclableItemRepository;
        private readonly IRecyclableTypeRepository _recyclableTypeRepository;

        public RecyclableItemController(IRecyclableItemRepository recyclableItemRepository, IRecyclableTypeRepository recyclableTypeRepository)
        {
            _recyclableItemRepository = recyclableItemRepository;
            _recyclableTypeRepository = recyclableTypeRepository;
        }

        public ActionResult Index()
        {
            var recyclableItems = _recyclableItemRepository.GetAll();
            return View(recyclableItems);
        }

        public ActionResult Details(int id)
        {
            var recyclableItem = _recyclableItemRepository.GetById(id);
            if (recyclableItem == null)
                return HttpNotFound();

            return View(recyclableItem);
        }

        public ActionResult Create()
        {
            ViewBag.RecyclableTypeId = new SelectList(_recyclableTypeRepository.GetAll(), "Id", "Type");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecyclableItem recyclableItem)
        {
            var recyclableType = _recyclableTypeRepository.GetById(recyclableItem.RecyclableTypeId);

            if (recyclableType == null)
            {
                ModelState.AddModelError("", "Invalid Recyclable Type.");
                ViewBag.RecyclableTypeId = new SelectList(_recyclableTypeRepository.GetAll(), "Id", "Type");
                return View(recyclableItem);
            }

            // Validate Weight
            if (recyclableItem.Weight < recyclableType.MinKg || recyclableItem.Weight > recyclableType.MaxKg)
            {
                ModelState.AddModelError("Weight", $"Weight must be between {recyclableType.MinKg} and {recyclableType.MaxKg} kg.");
                ViewBag.RecyclableTypeId = new SelectList(_recyclableTypeRepository.GetAll(), "Id", "Type");
                return View(recyclableItem);
            }

            recyclableItem.ComputedRate = recyclableType.Rate * recyclableItem.Weight;
            recyclableItem.ComputedRate = decimal.Round(recyclableItem.ComputedRate, 2);

            if (ModelState.IsValid)
            {
                _recyclableItemRepository.Add(recyclableItem);
                return RedirectToAction("Index");
            }

            ViewBag.RecyclableTypeId = new SelectList(_recyclableTypeRepository.GetAll(), "Id", "Type", recyclableItem.RecyclableTypeId);
            return View(recyclableItem);
        }

        public ActionResult Edit(int id)
        {
            var recyclableItem = _recyclableItemRepository.GetById(id);
            if (recyclableItem == null)
                return HttpNotFound();

            ViewBag.RecyclableTypeId = new SelectList(_recyclableTypeRepository.GetAll(), "Id", "Type", recyclableItem.RecyclableTypeId);
            return View(recyclableItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecyclableItem recyclableItem)
        {
            var recyclableType = _recyclableTypeRepository.GetById(recyclableItem.RecyclableTypeId);

            if (recyclableType == null)
            {
                ModelState.AddModelError("", "Invalid Recyclable Type.");
                ViewBag.RecyclableTypeId = new SelectList(_recyclableTypeRepository.GetAll(), "Id", "Type", recyclableItem.RecyclableTypeId);
                return View(recyclableItem);
            }

            // Validate Weight
            if (recyclableItem.Weight < recyclableType.MinKg || recyclableItem.Weight > recyclableType.MaxKg)
            {
                ModelState.AddModelError("Weight", $"Weight must be between {recyclableType.MinKg} and {recyclableType.MaxKg} kg.");
                ViewBag.RecyclableTypeId = new SelectList(_recyclableTypeRepository.GetAll(), "Id", "Type", recyclableItem.RecyclableTypeId);
                return View(recyclableItem);
            }

            recyclableItem.ComputedRate = recyclableType.Rate * recyclableItem.Weight;
            recyclableItem.ComputedRate = decimal.Round(recyclableItem.ComputedRate, 2);

            if (ModelState.IsValid)
            {
                _recyclableItemRepository.Update(recyclableItem);
                return RedirectToAction("Index");
            }

            ViewBag.RecyclableTypeId = new SelectList(_recyclableTypeRepository.GetAll(), "Id", "Type", recyclableItem.RecyclableTypeId);
            return View(recyclableItem);
        }

        public ActionResult Delete(int id)
        {
            var recyclableItem = _recyclableItemRepository.GetById(id);
            if (recyclableItem == null)
                return HttpNotFound();

            return View(recyclableItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _recyclableItemRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
