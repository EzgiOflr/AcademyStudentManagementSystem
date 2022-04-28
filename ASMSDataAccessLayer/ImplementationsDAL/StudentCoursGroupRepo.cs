using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMSDataAccessLayer.ContractsDAL;
using ASMSEntityLayer.Models;
namespace ASMSDataAccessLayer.ImplementationsDAL
{
    public class StudentCoursGroupRepo : RepositoryBase<StudentsCourseGroup, int>, IStudentsCourseGroupRepo
    {
        public StudentCoursGroupRepo(MyContext myContext) : base(myContext)
        {

        }
    }
}