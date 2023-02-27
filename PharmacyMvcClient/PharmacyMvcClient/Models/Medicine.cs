using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyMvcClient.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineID { get; set; }
        [Required,StringLength(50)]
        public string MedicineName { get; set; }
        [Required,StringLength(100)]
        public string MedicineDescription { get; set; }
        [Required]
        public string MedicinePrice { get; set; }
        public int CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public Company Companies { get; set; }
    }
}
