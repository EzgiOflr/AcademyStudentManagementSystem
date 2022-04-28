using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSDataAccessLayer.ContractsDAL
{
    public interface IUnitOfWork
    {
        ICityRepo CityRepo { get;  }
        IClassRepo ClassRepo { get;  }
        ICourseRepo CourseRepo { get;  }
        ICourseGroupRepo CourseGroupRepo { get;  }
        IDistrictRepo DistrictRepo { get; }
        INeighbourhoodRepo NeighbourhoodRepo { get; }
        IStudentRepo StudentRepo { get; }
        IStudentsCourseGroupRepo StudentsCourseGroupRepo { get; }
        ITeacherRepo TeacherRepo { get; }
        IUsersAddressRepo UsersAddressRepo { get; }
        IStudentAttendanceRepo StudentAttendanceRepo { get; }


    }
}
