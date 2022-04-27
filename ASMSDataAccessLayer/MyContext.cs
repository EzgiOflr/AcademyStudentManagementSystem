using ASMSEntityLayer.IdentityModels;
using ASMSEntityLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSDataAccessLayer
{
    public class MyContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public MyContext(DbContextOptions<MyContext>options) :base(options)
        {

        }
        //sql tablomdakilerin sanalını oluşturuyorum
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Neighbourhood> Neighbourhoods { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Class> Classes  { get; set; }
        public virtual DbSet<CourseGroup> CourseGroups { get; set; }
        public virtual DbSet<StudentsCourseGroup> StudentsCourseGroups { get; set; }
        public virtual DbSet<StudentAttendance> StudentAttendances { get; set; }
        public virtual DbSet<UsersAddress> UsersAddresses { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        //override unique yapma diğer yolu 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseGroup>()
                .HasIndex(cg => new { cg.PortalCode})
                .IsUnique(true);
                base.OnModelCreating(builder);


            ////ilişki burada da kurulabilir.
            //builder.Entity<District>().HasOne(d => d.City)//bire
            //                          .WithMany(c => c.Districts)//cok ilişki
            //                          .HasForeignKey(d=>d.CityId)//ne üzerinden
            //                          .OnDelete(DeleteBehavior.NoAction); //hangi
            //                                                              //davranışla(ilçe silinemez)

            ////ilişki burada da kurulabilir.(Modelde yaptıgımız)
            //builder.Entity<Neighbourhood>().HasOne(d => d.District)//bire
            //                          .WithMany(c => c.Neighbourhoods)//cok ilişki
            //                          .HasForeignKey(d => d.DistrictId)//ne üzerinden
            //                          .OnDelete(DeleteBehavior.NoAction); //hangi
            //                                                              //davranışla(ilçe silinemez)





        }



    }
}
