using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.ViewModels
{
   public class CityVM
    {
        public byte Id { get; set; }  
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; } 
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İl adı en az 2 , en fazla 50 karakter aralığında olmalıdır!")]
        public string CityName { get; set; }

        [Required]
        public byte PlateCode { get; set; }
        //buraya geri dönücem 
        public virtual ICollection<DistrictVM> Districts { get; set; }

    }
}
