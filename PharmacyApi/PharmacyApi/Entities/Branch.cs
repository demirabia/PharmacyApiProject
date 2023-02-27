using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacyApi.Entities
{
    public class Branch
    {
        [Key]
        public int BranchID { get; set; }
        [Required,StringLength(50)]
        public string BranchName { get; set; }
        [Required,StringLength(11)]
        public string BranchPhone { get; set; }
        [Required,StringLength(100)]
        public string BranchAdress { get; set; }
        
    }
}
