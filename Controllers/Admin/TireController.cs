using Microsoft.AspNetCore.Mvc;

namespace AutoPartsStore.Controllers.Admin
{
    public class TireController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
