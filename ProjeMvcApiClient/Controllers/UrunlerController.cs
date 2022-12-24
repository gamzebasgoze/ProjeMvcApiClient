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
    public class UrunlerController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44375/api/Urunler").Result;
            List<Urunler> Urunlers = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Urunlers = JsonConvert.DeserializeObject<List<Urunler>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(Urunlers);
        }

        public IActionResult Add()
        {
            return View(new Urunler());
        }

        [HttpPost]
        public IActionResult Add(Urunler urunler)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(urunler), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync($"https://localhost:44375/api/Urunler", content).Result;
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
            var responseMessage = httpClient.GetAsync($"https://localhost:44375/api/Urunler/{id}").Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var Urunler = JsonConvert.DeserializeObject<Urunler>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(Urunler);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public IActionResult Edit(Urunler urunler)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(urunler), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44375/api/Urunler/{urunler.UrunId}", content).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"https://localhost:44375/api/Urunler/{id}").Result;
            return RedirectToAction("Index");
        }
    }
}
