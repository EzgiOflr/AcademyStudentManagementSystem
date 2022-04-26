using ASMSEntityLayer.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    [Table("UserAdresses")]
    public class UsersAddress : Base<int>
    {
        public string UserId { get; set; }  //AspnetUsers ilişli
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Adres başlığı en az 2 , en fazla 50 karakter aralığında olmalıdır!")]
        public string AddressTitle { get; set; }
        public int NeighbourhoodId { get; set; }  //mahalleyle ilişki
        [Required]
        [StringLength(500, ErrorMessage = "Adres detayı en fazla 500 karakter aralığında olabilir!")]
        public string AddressDetails { get; set; }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "Posta kodu 5 karakter olmalıdır!!")]
        public string PostCode { get; set; }

        //ilişkiler 

        [ForeignKey("UserId")]

        public virtual AppUser AppUser { get; set; }

        [ForeignKey("NeighbourhoodId")]
        public virtual Neighbourhood Neighbourhood { get; set; }
    }
}
