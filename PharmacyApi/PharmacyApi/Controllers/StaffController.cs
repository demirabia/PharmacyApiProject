using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Entities;
using System.Linq;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public StaffController(ApplicationContext context)
        {
            _context = context;

        }
        [HttpGet] //okuma listeleme
        public IActionResult Get()
        {
            return Ok(_context.Staffs.ToList());
        }


        [HttpPut("{id}")] //yenileme ID e göre yapacağız anlamında
        public IActionResult Update(int id, Staff staff)
        {
            //ID ye göre veritabanına göre git getir ve o satırdaki verileri benim yeni girdiğim bilgilerle değiştir ve kaydet Api de görter.
            var update = _context.Staffs.FirstOrDefault(I => I.BranchID == id);
            update.StaffName = staff.StaffName;
            update.StaffAge = staff.StaffAge;
            update.StaffPhone = staff.StaffPhone;
            update.StaffAdress = staff.StaffAdress;
            update.StaffSalary = staff.StaffSalary;
            update.BranchID = staff.BranchID;


            _context.SaveChanges();
            return NoContent();//view yapısı yok var olan sayfadaki alanı döndürüyor ve sayfa belirtmk zorunda olduğumuz için bu şekilde belirttik.
        }

        [HttpDelete("{id}")] //silme ID e göre silme işlemi yapacak anlamında.
        public IActionResult Delete(int id)
        {
            var delete = _context.Staffs.FirstOrDefault(I => I.StaffID == id);
            _context.Remove(delete);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost] //ekleme
        public IActionResult Add(Staff staff)
        {
            _context.Add(staff);
            _context.SaveChanges();
            return Created("", staff);
        }

        //api/products/GetsprById/1
        [HttpGet("{id}")] //arama
        public IActionResult GetById(int id)
        {
            return Ok(_context.Staffs.FirstOrDefault(I => I.StaffID == id));
        }
    }
}
