using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    // [Index(nameof(PortalCode),IsUnique=true)] Context classında OnModelCreating metodu ezerek yazacağız.
    [Table("CoursesGroup")]
    public class CourseGroup : Base<int>
    {
        //eğitimler tabllosu
        public int ClassId { get; set; }
        public string TeacherTCNumber { get; set; }
        public int CourseId { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime FinishDate { get; set; }
        public int Capasite { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Kurs portal numarası 7 haneli olmalıdır!!")]

        
        public string PortalCode { get; set; }

        //ilişki
        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
        [ForeignKey("TeacherTCNumber")]
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        //ilişki karşılığı eğitimi alan öğrenciler listesi 
        public virtual ICollection<StudentsCourseGroup> Students { get; set; }
    }

}
