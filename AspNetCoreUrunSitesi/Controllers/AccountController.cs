using AspNetCoreUrunSitesi.Models;
using BL;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreUrunSitesi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<AppUser> _repository;

        public AccountController(IRepository<AppUser> repository)
        {
            _repository = repository;
        }
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.User;
            if (user != null)
            {
                //ViewBag.Usr = user.FindFirstValue("userId");
                var usr = await _repository.FindAsync(int.Parse(user.FindFirstValue("userId")));
                if (usr is not null)
                {
                    return View(usr);
                }
            }
            return View();
        }
        [Authorize(Policy = "UserPolicy"), HttpPost]
        public IActionResult Index(AppUser appUser)
        {

            return View();
        }
        /*public IActionResult Index()
        {
            if (!HttpContext.Session.GetInt32("login").HasValue)
            {
                return RedirectToAction("Login");
            }
            return View();
        }*/
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
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
                    return RedirectToAction("Index");
                }
            }
            return View(loginViewModel);
        }
        public IActionResult UserLogin(string ReturnUrl)
        {
            ViewBag.DonusUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserLoginAsync(LoginViewModel loginViewModel, string ReturnUrl)
        {
            try
            {
                var account = _repository.Get(x => x.Username == loginViewModel.Username & x.Password == loginViewModel.Password & x.IsActive);
                if (account == null) // Girilen bilgilere göre eşleşen kayıt yoksa
                {
                    ModelState.AddModelError("", "Giriş Başarısız!");
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim("Role", account.IsAdmin ? "Admin" : "User"),
                        new Claim("UserId", account.Id.ToString())
                    };
                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    if (account.IsAdmin) return Redirect(!string.IsNullOrWhiteSpace(ReturnUrl) ? ReturnUrl : "/Admin/");
                    else return RedirectToAction("Index", "Account");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(loginViewModel);
        }
        [Route("Logout")] // Bu adrese gelen istek olursa çıkış yap
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("UserLogin", "Account");
        }
    }
}
