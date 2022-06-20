using AspNetCoreUrunSitesi.Models;
using BL;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreUrunSitesi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<AppUser> _repository;

        public AccountController(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            if (!HttpContext.Session.GetInt32("login").HasValue)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                var account = _repository.Get(x => x.Username == loginViewModel.Username & x.Password == loginViewModel.Password & x.IsActive);
                if (account == null) // Girilen bilgilere göre eşleşen kayıt yoksa
                {
                    ModelState.AddModelError("", "Giriş Başarısız!");
                }
                else
                {
                    //HttpContext.Session.SetString("login", account.Id.ToString());
                    HttpContext.Session.SetInt32("login", account.Id);
                }
            }
            return View(loginViewModel);
        }
    }
}
