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
    public class PersonellerController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44375/api/Personeller").Result;
            List<Personeller> Personellers = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Personellers = JsonConvert.DeserializeObject<List<Personeller>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(Personellers);
        }

        public IActionResult Add()
        {
            return View(new Personeller());
        }

        [HttpPost]
        public IActionResult Add(Personeller personeller)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(personeller), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync($"https://localhost:44375/api/Personeller", content).Result;
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
            var responseMessage = httpClient.GetAsync($"https://localhost:44375/api/Personeller/{id}").Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var Personeller = JsonConvert.DeserializeObject<Personeller>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(Personeller);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public IActionResult Edit(Personeller personeller)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(personeller), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44375/api/Personeller/{personeller.PersonelId}", content).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"https://localhost:44375/api/Personeller/{id}").Result;
            return RedirectToAction("Index");
        }
    }
}
