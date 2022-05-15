using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreUrunSitesi.Areas.ApiAdmin.Controllers
{
    [Area("ApiAdmin")]//Authorize
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
