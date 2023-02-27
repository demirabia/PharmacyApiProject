using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyMvcClient.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace PharmacyMvcClient.Controllers
{
    public class HMedicineController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync("https://localhost:44320/api/Medicine").Result;
            List<Medicine> medicines = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //DeserializeObject :json formatındaki değerleri nesneye dönüştürmeye denir.
                //SerializeObject :nesneyi json formatna çevirir.
                medicines = JsonConvert.DeserializeObject<List<Medicine>>(responseMessage.Content.ReadAsStringAsync().Result);
            }
            return View(medicines);
        }
    }
}
