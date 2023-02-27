using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMvcClient.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace PharmacyMvcClient.Controllers
{
    public class MedicineController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44320/api/Medicine").Result;
            List<Medicine> medicine = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //DeserializeObject :json formatındaki değerleri nesneye dönüştürmeye denir.
                //SerializeObject :nesneyi json formatna çevirir.
                medicine = JsonConvert.DeserializeObject<List<Medicine>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(medicine);
        }
        public IActionResult Add()
        {
            return View(new Medicine());
        }
        [HttpPost]
        public IActionResult Add(Medicine medicines)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(medicines), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync("https://localhost:44320/api/Medicine", content).Result;
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
            var responseMessage = httpClient.GetAsync($"https://localhost:44320/api/Medicine/{id}").Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var medicines = JsonConvert.DeserializeObject<Medicine>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(medicines);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Medicine medicines)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(medicines), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44320/api/Medicine/{medicines.MedicineID}", content).Result;
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"https://localhost:44320/api/Medicine/{id}").Result;
            return RedirectToAction("Index");

        }
    }
}
