using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacyApi.Entities
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        [Required,StringLength(50)]
        public string CompanyName { get; set; }
        [Required,StringLength(11)]
        public string CompanyPhone { get; set; }
        [Required,StringLength(100)]
        public string CompanyAdress { get; set;}
       
    }
}
