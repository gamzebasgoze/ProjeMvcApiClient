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
    public class DepolarController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44375/api/Depolar").Result;
            List<Depolar> Depolars = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Depolars = JsonConvert.DeserializeObject<List<Depolar>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(Depolars);
        }

        public IActionResult Add()
        {
            return View(new Depolar());
        }

        [HttpPost]
        public IActionResult Add(Depolar depolar)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(depolar), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync($"https://localhost:44375/api/Depolar", content).Result;
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
            var responseMessage = httpClient.GetAsync($"https://localhost:44375/api/Depolar/{id}").Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var Depolar = JsonConvert.DeserializeObject<Depolar>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(Depolar);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public IActionResult Edit(Depolar depolar)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(depolar), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44375/api/Depolar/{depolar.DepoId}", content).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"https://localhost:44375/api/Depolar/{id}").Result;
            return RedirectToAction("Index");
        }
    }
}
