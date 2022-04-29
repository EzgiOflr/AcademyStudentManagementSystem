using ASMSEntityLayer.IdentityModels;
using ASMSEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.ViewModels
{
    public class UsersAddressVM
    {
        //Base ve modeldeki UsersAddressdeki herseyi burada birleştirdik.
        public int Id { get; set; } //view model old icin ıd si int
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2,
        ErrorMessage = "Adres başlığı en az 2 , en fazla 50 karakter aralığında olmalıdır!")]
        public string AddressTitle { get; set; }
        [Required(ErrorMessage ="Mahalle seçimi gereklidir!")]
        public int NeighbourhoodId { get; set; }
        [StringLength(500, ErrorMessage = "Adres detayı en fazla 500 karakter aralığında olabilir!")]
        public string AddressDetails { get; set; }
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Posta kodu 5 karakter olmalıdır!!")]
        public string PostCode { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual Neighbourhood Neighbourhood { get; set; }//include entities
        

        //TODO: aşağıdakiler ile il ve ilçeye ulaşabilir miyim?
        public City City { get; set; }
        public District District { get; set; }
    }
}
//bllde entityinin kendisi yok entity dal da oldugu icin mapleme yapıyoruz