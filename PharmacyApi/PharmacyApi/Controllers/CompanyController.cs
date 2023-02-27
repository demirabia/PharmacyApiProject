using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Entities;
using System.Linq;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public CompanyController(ApplicationContext context)
        {
            _context = context;

        }
        [HttpGet] //okuma listeleme
        public IActionResult Get()
        {
            return Ok(_context.Companies.ToList());
        }


        [HttpPut("{id}")] //yenileme ID e göre yapacağız anlamında
        public IActionResult Update(int id, Company company)
        {
            //ID ye göre veritabanına göre git getir ve o satırdaki verileri benim yeni girdiğim bilgilerle değiştir ve kaydet Api de görter.
            var update = _context.Companies.FirstOrDefault(I => I.CompanyID == id);
            update.CompanyName = company.CompanyName;
            update.CompanyPhone = company.CompanyPhone;
            update.CompanyAdress = company.CompanyAdress;
            _context.SaveChanges();
            return NoContent();//view yapısı yok var olan sayfadaki alanı döndürüyor ve sayfa belirtmk zorunda olduğumuz için bu şekilde belirttik.
        }

        [HttpDelete("{id}")] //silme ID e göre silme işlemi yapacak anlamında.
        public IActionResult Delete(int id)
        {
            var delete = _context.Companies.FirstOrDefault(I => I.CompanyID == id);
            _context.Remove(delete);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost] //ekleme
        public IActionResult Add(Company company)
        {
            _context.Add(company);
            _context.SaveChanges();
            return Created("", company);
        }

        //api/products/GetsprById/1
        [HttpGet("{id}")] //arama
        public IActionResult GetById(int id)
        {
            return Ok(_context.Companies.FirstOrDefault(I => I.CompanyID == id));
        }
    }
}
