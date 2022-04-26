using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    [Table("Districts")]
    public class District : Base<int>
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Il adı en az 2 , en fazla 50 karakter aralığında olmalıdır!")]
        public string DistrictName { get; set; }

        //ilişki kurulucak
        public byte CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public virtual ICollection<Neighbourhood> Neighbourhoods { get; set; }

    }
}
