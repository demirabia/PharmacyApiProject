using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMvcClient.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace PharmacyMvcClient.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44320/api/Company").Result;
            List<Company> companies = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //DeserializeObject :json formatındaki değerleri nesneye dönüştürmeye denir.
                //SerializeObject :nesneyi json formatna çevirir.
                companies = JsonConvert.DeserializeObject<List<Company>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(companies);
        }
        public IActionResult Add()
        {
            return View(new Company());
        }
        [HttpPost]
        public IActionResult Add(Company companies)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(companies), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync("https://localhost:44320/api/Company", content).Result;
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
            var responseMessage = httpClient.GetAsync($"https://localhost:44320/api/Company/{id}").Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var companies = JsonConvert.DeserializeObject<Company>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(companies);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Company companies)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(companies), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44320/api/Company/{companies.CompanyID}", content).Result;
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"https://localhost:44320/api/Company/{id}").Result;
            return RedirectToAction("Index");

        }
    }
}
