using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoPartsStore.Controllers.Admin
{
    public class OilController : Controller
    {
        // GET: OilController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OilController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OilController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OilController/Create
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

        // GET: OilController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OilController/Edit/5
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

        // GET: OilController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OilController/Delete/5
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
