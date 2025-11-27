using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoPartsStore.Controllers
{
    public class TiresController : Controller
    {
        // GET: TiresController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TiresController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TiresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiresController/Create
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

        // GET: TiresController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TiresController/Edit/5
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

        // GET: TiresController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TiresController/Delete/5
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
