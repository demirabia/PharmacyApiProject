using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Entities;
using System.Linq;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public MedicineController(ApplicationContext context)
        {
            _context = context;

        }
        [HttpGet] //okuma listeleme
        public IActionResult Get()
        {
            return Ok(_context.Medicines.ToList());
        }


        [HttpPut("{id}")] //yenileme ID e göre yapacağız anlamında
        public IActionResult Update(int id, Medicine medicine)
        {
            //ID ye göre veritabanına göre git getir ve o satırdaki verileri benim yeni girdiğim bilgilerle değiştir ve kaydet Api de görter.
            var update = _context.Medicines.FirstOrDefault(I => I.MedicineID == id);
            update.MedicineName = medicine.MedicineName;
            update.MedicineDescription = medicine.MedicineDescription;
            update.MedicinePrice = medicine.MedicinePrice;
            update.CompanyID = medicine.CompanyID;
            _context.SaveChanges();
            return NoContent();//view yapısı yok var olan sayfadaki alanı döndürüyor ve sayfa belirtmk zorunda olduğumuz için bu şekilde belirttik.
        }

        [HttpDelete("{id}")] //silme ID e göre silme işlemi yapacak anlamında.
        public IActionResult Delete(int id)
        {
            var delete = _context.Medicines.FirstOrDefault(I => I.MedicineID == id);
            _context.Remove(delete);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost] //ekleme
        public IActionResult Add(Medicine medicine)

        {
            _context.Add(medicine);
            _context.SaveChanges();
            return Created("", medicine);
        }

        //api/products/GetsprById/1
        [HttpGet("{id}")] //arama
        public IActionResult GetById(int id)
        {
            return Ok(_context.Medicines.FirstOrDefault(I => I.MedicineID == id));
        }
    }
}
