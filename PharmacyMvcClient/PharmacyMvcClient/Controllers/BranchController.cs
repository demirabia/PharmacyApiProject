using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMvcClient.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace PharmacyMvcClient.Controllers
{
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44320/api/Branch").Result;
            List<Branch> branches = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //DeserializeObject :json formatındaki değerleri nesneye dönüştürmeye denir.
                //SerializeObject :nesneyi json formatna çevirir.
                branches = JsonConvert.DeserializeObject<List<Branch>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(branches);
        }
        public IActionResult Add()
        {
            return View(new Branch());
        }
        [HttpPost]
        public IActionResult Add(Branch branches)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(branches), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync("https://localhost:44320/api/Branch", content).Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Ekleme İşlemi Başarısız");
            return View();
        }
        public IActionResult Edit(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.GetAsync($"https://localhost:44320/api/Branch/{id}").Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var branches = JsonConvert.DeserializeObject<Branch>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(branches);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Branch branches)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(branches), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44320/api/Branch/{branches.BranchID}", content).Result;
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"https://localhost:44320/api/Branch/{id}").Result;
            return RedirectToAction("Index");

        }
    }
}
