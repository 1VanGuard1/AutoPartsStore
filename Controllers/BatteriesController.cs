using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoPartsStore.Controllers
{
    public class BatteriesController : Controller
    {
        // GET: BatteriesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BatteriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BatteriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BatteriesController/Create
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

        // GET: BatteriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BatteriesController/Edit/5
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

        // GET: BatteriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BatteriesController/Delete/5
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
