using Microsoft.AspNetCore.Mvc;

namespace AutoPartsStore.Controllers.Admin
{
    public class WiperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
