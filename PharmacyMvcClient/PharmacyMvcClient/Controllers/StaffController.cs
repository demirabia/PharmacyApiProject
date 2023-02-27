using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMvcClient.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace PharmacyMvcClient.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44320/api/Staff").Result;
            List<Staff> Staffes = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //DeserializeObject :json formatındaki değerleri nesneye dönüştürmeye denir.
                //SerializeObject :nesneyi json formatna çevirir.
                Staffes = JsonConvert.DeserializeObject<List<Staff>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(Staffes);
        }
        public IActionResult Add()
        {
            return View(new Staff());
        }
        [HttpPost]
        public IActionResult Add(Staff Staffes)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(Staffes), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync("https://localhost:44320/api/Staff", content).Result;
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
            var responseMessage = httpClient.GetAsync($"https://localhost:44320/api/Staff/{id}").Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var Staffes = JsonConvert.DeserializeObject<Staff>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(Staffes);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Staff Staffes)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(Staffes), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44320/api/Staff/{Staffes.StaffID}", content).Result;
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"https://localhost:44320/api/Staff/{id}").Result;
            return RedirectToAction("Index");

        }
    }
}
