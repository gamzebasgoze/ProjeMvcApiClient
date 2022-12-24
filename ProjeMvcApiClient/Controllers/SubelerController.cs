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
    public class SubelerController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44375/api/Subeler").Result;
            List<Subeler> Subelers = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Subelers = JsonConvert.DeserializeObject<List<Subeler>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(Subelers);
        }

        public IActionResult Add()
        {
            return View(new Subeler());
        }

        [HttpPost]
        public IActionResult Add(Subeler subeler)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(subeler), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync($"https://localhost:44375/api/Subeler", content).Result;
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
            var responseMessage = httpClient.GetAsync($"https://localhost:44375/api/Subeler/{id}").Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var Subeler = JsonConvert.DeserializeObject<Subeler>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(Subeler);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public IActionResult Edit(Subeler subeler)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(subeler), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44375/api/Subeler/{subeler.SubeId}", content).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"https://localhost:44375/api/Subeler/{id}").Result;
            return RedirectToAction("Index");
        }
    }
}
