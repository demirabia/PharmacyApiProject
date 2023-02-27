using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyApi.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Required,StringLength(50)]
        public string CustomerName { get; set; }
        [Required,StringLength(11)]
        public string CustomerPhone { get; set; }
        public int MedicineID { get; set; }
        [ForeignKey("MedicineID")]
        public Medicine Medicines { get; set; }
        public int StaffID { get; set; }
        [ForeignKey("StaffID")]
        public Staff Staffs { get; set; }
    }
}
