using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyApi.Entities
{
    public class Staff
    {
        [Key]
        public int StaffID { get; set; }
        [Required,StringLength(50)]
        public string StaffName { get; set; }
        [Required]
        public string StaffAge { get; set; }
        [Required,StringLength(11)]
        public string StaffPhone { get; set; }
        [Required,StringLength(100)]
        public string StaffAdress { get; set; }
        [Required]
        public string StaffSalary { get; set; }
        public int BranchID { get; set; }
        [ForeignKey("BranchID")]
        public Branch Branches { get; set; }
    }
}
