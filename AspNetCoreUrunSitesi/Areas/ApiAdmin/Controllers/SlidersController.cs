using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Entities;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreUrunSitesi.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace AspNetCoreUrunSitesi.Areas.ApiAdmin.Controllers
{
    [Area("ApiAdmin"), Authorize]
    public class SlidersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SlidersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        string apiAdres = "https://localhost:44395/api/sliders";
        string uplAdres = "https://localhost:44395/api/upload";

        // GET: SlidersController
        public async Task<ActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiAdres);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Slider>>(jsonData);
                return View(result);
            }
            return View(null);
        }

        // GET: SlidersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlidersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Slider slider, IFormFile Image)
        {
            //var bytes = await System.IO.File.ReadAllBytesAsync(Image.FileName);
            var stream = new MemoryStream();
            await Image.CopyToAsync(stream);
            var bytes = stream.ToArray();

            ByteArrayContent fileContent = new(bytes);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Image.ContentType);

            MultipartFormDataContent formData = new()
            {
                { fileContent, "file", Image.FileName }
            };

            using var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization.Parameter.Insert(0, "");
            var responseMessage = await httpClient.PostAsync(uplAdres, formData);

            if (responseMessage.IsSuccessStatusCode)
            {
                try
                {
                    using var client = _httpClientFactory.CreateClient();
                    slider.Image = Image.FileName;
                    var jsonData = JsonConvert.SerializeObject(slider);
                    StringContent stringContent = new(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
                    var resMessage = await client.PostAsync(apiAdres, stringContent);
                    if (resMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else ModelState.AddModelError("", $"Post İsteğinde Hata Oluştu! Hata Kodu : {(int)resMessage.StatusCode}");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ModelState.AddModelError("", "bir hata oluştu");

            return View(slider);
        }

        // GET: SlidersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiAdres + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Slider>(jsonData);
                return View(data);
            }
            return View();
        }

        // POST: SlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Slider slider, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = _httpClientFactory.CreateClient();
                    if (Image != null) slider.Image = FileHelper.FileLoader(Image);
                    var jsonData = JsonConvert.SerializeObject(slider);
                    StringContent stringContent = new(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
                    var responseMessage = await client.PutAsync(apiAdres, stringContent);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else ModelState.AddModelError("", $"Post İsteğinde Hata Oluştu! Hata Kodu : {(int)responseMessage.StatusCode}");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(slider);
        }

        // GET: SlidersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiAdres + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Slider>(jsonData);
                return View(data);
            }
            return View();
        }

        // POST: SlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                await client.DeleteAsync(apiAdres + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
