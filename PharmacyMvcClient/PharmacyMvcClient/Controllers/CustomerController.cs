using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMvcClient.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace PharmacyMvcClient.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44320/api/Customer").Result;
            List<Customer> customer = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //DeserializeObject :json formatındaki değerleri nesneye dönüştürmeye denir.
                //SerializeObject :nesneyi json formatna çevirir.
                customer = JsonConvert.DeserializeObject<List<Customer>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(customer);
        }
        public IActionResult Add()
        {
            return View(new Customer());
        }
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(customer), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync("https://localhost:44320/api/Customer", content).Result;
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
            var responseMessage = httpClient.GetAsync($"https://localhost:44320/api/Customer/{id}").Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var customers = JsonConvert.DeserializeObject<Customer>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(customers);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Customer customers)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(customers), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PutAsync($"https://localhost:44320/api/Customer/{customers.CustomerID}", content).Result;
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.DeleteAsync($"https://localhost:44320/api/Customer/{id}").Result;
            return RedirectToAction("Index");

        }
    }
}
