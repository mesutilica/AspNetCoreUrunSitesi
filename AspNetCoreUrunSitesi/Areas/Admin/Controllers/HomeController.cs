using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreUrunSitesi.Areas.Admin.Controllers
{
    [Area("Admin")] // admin area içindeki controller ların çalışması için bu gerekli!!
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
