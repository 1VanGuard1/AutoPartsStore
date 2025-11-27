using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoPartsStore.Controllers
{
    public class WipersController : Controller
    {
        // GET: WipersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WipersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WipersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WipersController/Create
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

        // GET: WipersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WipersController/Edit/5
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

        // GET: WipersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WipersController/Delete/5
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
