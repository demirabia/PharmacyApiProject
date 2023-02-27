using Microsoft.AspNetCore.Mvc;

namespace PharmacyMvcClient.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
