using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Enums
{
    class AllEnums
    {
    }
    public enum Genders : byte
    {
        Male = 0 ,
        Female= 1,
        Unknown= 2 
    }

    public enum ClassLocation : byte
    {
        GirisKat=0,
        kat1=1,
        kat2=2,
        kat3=3,
        kat4=4,
        kat5=5
    }
    public enum ASMSRoles : byte
    {
        Passive=0,
        Student=1,
        Teacher=2,
        Coordinator=3,
        StudentAdministration=4,
        Manager=5

    }

}
