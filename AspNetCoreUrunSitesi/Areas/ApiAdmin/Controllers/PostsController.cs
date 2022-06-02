using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Entities;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreUrunSitesi.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreUrunSitesi.Areas.ApiAdmin.Controllers
{
    [Area("ApiAdmin"), Authorize]
    public class PostsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PostsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        string apiAdres = "https://localhost:44395/api/Posts";
        string apiKategoriAdres = "https://localhost:44395/api/Categories";

        // GET: PostsController
        public async Task<ActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiAdres);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Post>>(jsonData);
                return View(result);
            }
            return View(null);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostsController/Create
        public async Task<ActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiKategoriAdres);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Category>>(jsonData);
                ViewBag.CategoryId = new SelectList(result, "Id", "Name");
            }
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post post, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = _httpClientFactory.CreateClient();
                    post.Image = FileHelper.FileLoader(Image);
                    var jsonData = JsonConvert.SerializeObject(post);
                    StringContent stringContent = new(jsonData, encoding: Encoding.UTF8, mediaType: "application/json");
                    var responseMessage = await client.PostAsync(apiAdres, stringContent);
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
            return View(post);
        }

        // GET: PostsController/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiAdres + "/" + id);
            var categoryList = await client.GetAsync(apiKategoriAdres);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Post>(jsonData);

                var categoryListjsonData = await categoryList.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Category>>(categoryListjsonData);
                ViewBag.CategoryId = new SelectList(result, "Id", "Name");
                return View(data);
            }
            return View();
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Post post, IFormFile Image, bool resmiSil)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var client = _httpClientFactory.CreateClient();
                    if (Image != null) post.Image = FileHelper.FileLoader(Image);
                    if (resmiSil == true)
                    {
                        FileHelper.FileRemover(fileName: post.Image);
                        post.Image = string.Empty;
                    }
                    var jsonData = JsonConvert.SerializeObject(post);
                    StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
                    var responseMessage = await client.PutAsync(apiAdres + "/" + id, stringContent);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else ModelState.AddModelError("", "Kayıt Güncellenemedi!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu! Kayıt Güncellenemedi!");
                }
            }
            return View(post);
        }

        // GET: PostsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(apiAdres + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Post>(jsonData);
                return View(data);
            }
            return View();
        }

        // POST: PostsController/Delete/5
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
