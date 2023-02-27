using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Entities;
using System.Linq;

namespace PharmacyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
            private readonly ApplicationContext _context;
            public BranchController(ApplicationContext context)
            {
                _context = context;

            }
            [HttpGet] //okuma listeleme
            public IActionResult Get()
            {
                return Ok(_context.Branches.ToList());
            }


            [HttpPut("{id}")] //yenileme ID e göre yapacağız anlamında
            public IActionResult Update(int id, Branch branch)
            {
                //ID ye göre veritabanına göre git getir ve o satırdaki verileri benim yeni girdiğim bilgilerle değiştir ve kaydet Api de görter.
                var update = _context.Branches.FirstOrDefault(I => I.BranchID == id);
                update.BranchName = branch.BranchName;
                update.BranchPhone = branch.BranchPhone;
                update.BranchAdress = branch.BranchAdress;

                _context.SaveChanges();
                return NoContent();//view yapısı yok var olan sayfadaki alanı döndürüyor ve sayfa belirtmk zorunda olduğumuz için bu şekilde belirttik.
            }

            [HttpDelete("{id}")] //silme ID e göre silme işlemi yapacak anlamında.
            public IActionResult Delete(int id)
            {
                var delete = _context.Branches.FirstOrDefault(I => I.BranchID == id);
                _context.Remove(delete);
                _context.SaveChanges();
                return NoContent();
            }
            [HttpPost] //ekleme
            public IActionResult Add(Branch branch)
            {
                _context.Add(branch);
                _context.SaveChanges();
                return Created("", branch);
            }

            //api/products/GetsprById/1
            [HttpGet("{id}")] //arama
            public IActionResult GetById(int id)
            {
                return Ok(_context.Branches.FirstOrDefault(I => I.BranchID == id));
            }
     }
}
