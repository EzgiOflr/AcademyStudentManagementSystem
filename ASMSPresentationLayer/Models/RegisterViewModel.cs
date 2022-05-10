using ASMSEntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASMSPresentationLayer.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik numarası 11 haneli olmalıdır.")]
        [Display(Name="Tc Kimlik Numarası")]
        public string TCNumber { get; set; }

        [Required(ErrorMessage = "İsim Gereklidir!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İsminiz en az 2, en fazla 50 karakter olmalıdır!")]
        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim Gereklidir!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyisminiz en az 2, en fazla 50 karakter olmalıdır!")]
        [Display(Name = "Soyisim")]
        public string Surname { get; set; }

        
        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage ="Email zorunludur!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Şifre alanı zorunludur!")]
        [StringLength(20,MinimumLength =8,ErrorMessage ="Şifreniz minimum 8 maksimum 20 haneli olmalıdır!")]
        [Display(Name ="Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Doğum tarihiniz")]
        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "Cinsiyet seçimi gereklidir!")]
        [Display(Name = "Cinsiyet")]
        public Genders Gender { get; set; }
    }
}
