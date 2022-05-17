using BL;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreUrunSitesi.Areas.ApiAdmin.Controllers
{
    [Area("ApiAdmin")]
    public class AppUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AppUserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



        // GET: AppUserController
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44395/api/AppUsers"); //https://localhost:44395/api/AppUsers
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<AppUser>>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
        }

        // GET: AppUserController/Details/5
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44395/api/AppUsers/" + id);
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AppUser>(jsonData);
                return View(result);
            }
            else
            {
                return View(null);
            }
        }

        // GET: AppUserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(appUser);
                StringContent stringContent = new(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
                var responseMessage = await client.PostAsync("https://localhost:44395/api/AppUsers/", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = $"Hata Oluştu! Kayıt Eklenemedi!. Hata Kodu: {(int)responseMessage.StatusCode}";
                }
            }
            return View(appUser);
        }

        // GET: AppUserController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44395/api/AppUsers/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<AppUser>(jsonData);
                return View(data);
            }
            else return NotFound();
        }

        // POST: AppUserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(appUser);
                StringContent stringContent = new(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
                var responseMessage = await client.PutAsync($"https://localhost:44395/api/AppUsers/{id}", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = $"Hata Oluştu! Kayıt Güncellenemedi!. Hata Kodu: {(int)responseMessage.StatusCode}";
                }
            }
            return View(appUser);
        }

        // GET: AppUserController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44395/api/AppUsers/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<AppUser>(jsonData);
                return View(data);
            }
            else return NotFound();
        }

        // POST: AppUserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:44395/api/AppUsers/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
