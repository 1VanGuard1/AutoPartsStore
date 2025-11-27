using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoPartsStore.Controllers
{
    public class MotorOilsController : Controller
    {
        // GET: MotorOilsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MotorOilsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MotorOilsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MotorOilsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MotorOilsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MotorOilsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MotorOilsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MotorOilsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
