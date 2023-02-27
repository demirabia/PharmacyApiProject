using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Entities;
using System.Linq;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public CustomerController(ApplicationContext context)
        {
            _context = context;

        }
        [HttpGet] //okuma listeleme
        public IActionResult Get()
        {
            return Ok(_context.Customers.ToList());
        }


        [HttpPut("{id}")] //yenileme ID e göre yapacağız anlamında
        public IActionResult Update(int id, Customer customer)
        {
            //ID ye göre veritabanına göre git getir ve o satırdaki verileri benim yeni girdiğim bilgilerle değiştir ve kaydet Api de görter.
            var update = _context.Customers.FirstOrDefault(I => I.CustomerID == id);
            update.CustomerName = customer.CustomerName;
            update.CustomerPhone = customer.CustomerPhone;
            update.MedicineID = customer.MedicineID;
            update.StaffID = customer.StaffID;
            _context.SaveChanges();
            return NoContent();//view yapısı yok var olan sayfadaki alanı döndürüyor ve sayfa belirtmk zorunda olduğumuz için bu şekilde belirttik.
        }

        [HttpDelete("{id}")] //silme ID e göre silme işlemi yapacak anlamında.
        public IActionResult Delete(int id)
        {
            var delete = _context.Customers.FirstOrDefault(I => I.CustomerID == id);
            _context.Remove(delete);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost] //ekleme
        public IActionResult Add(Customer customer)

        {
            _context.Add(customer);
            _context.SaveChanges();
            return Created("", customer);
        }

        //api/products/GetsprById/1
        [HttpGet("{id}")] //arama
        public IActionResult GetById(int id)
        {
            return Ok(_context.Customers.FirstOrDefault(I => I.CustomerID == id));
        }
    }
}
