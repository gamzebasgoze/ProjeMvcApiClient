using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjeMvcApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjeMvcApiClient.Controllers
{
    public class KategorilerController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44375/api/Kategoriler").Result;
            List<Kategoriler> Kategorilers = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Kategorilers = JsonConvert.DeserializeObject<List<Kategoriler>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(Kategorilers);
        }

        public IActionResult Add()
        {
            return View(new Kategoriler());
        }

        [HttpPost]
        public IActionResult Add(Kategoriler kategoriler)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(kategoriler), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync($"https://localhost:44375/api/Kategoriler", content).Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Ekleme işlemi başarısız");
            return View();
        }

        public IActionResult Edit(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.GetAsync($"https://localhost:44375/api/Kategoriler/{id}").Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var Kategoriler = JsonConvert.DeserializeObject<Kategoriler>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(Kategoriler);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public IActionResult Edit(Kategoriler kategoriler)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(kategoriler), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44375/api/Kategoriler/{kategoriler.KategoriId}", content).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"https://localhost:44375/api/Kategoriler/{id}").Result;
            return RedirectToAction("Index");
        }
    }
}
