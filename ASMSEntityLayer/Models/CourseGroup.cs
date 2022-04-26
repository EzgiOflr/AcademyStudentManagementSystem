using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    [Table("CoursesGroup")]
    class CourseGroup :Base<int>
    {
        public int ClassId { get; set; }
        //öğretmen id gelecek
        public int CourseId { get; set; }
        public DateTime FinishDate { get; set; }
        public int Capasite { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Kurs portal numarası 7 haneli olmalıdır!!")]
        //TODO isunique eklensin
        public string PortalCode { get; set; }//1090997  1101064

    }
}
