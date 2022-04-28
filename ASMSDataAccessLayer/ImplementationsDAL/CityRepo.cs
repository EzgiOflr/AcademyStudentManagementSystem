using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMSDataAccessLayer.ContractsDAL;
using ASMSEntityLayer.Models;

namespace ASMSDataAccessLayer.ImplementationsDAL
{
    public class CityRepo:RepositoryBase<City,byte>,ICityRepo
    {
        //ICityRepo kalıtım alma sebebi DI yapısına uygun calısmak
        //RepositoryBase den kalıntı alma sebebi içindeki CRUD metotlarını kullanabilmek 

        public CityRepo(MyContext myContext):base (myContext)
        {
            //ctor oluşturma sebebi
            //kalıtım aldıgımız classın ctor'ında myContext istendiği için
            //diğer tarafda contructurin kalıtımında paramete oldugu icin burda tekrar ctor yaptık.(repositoryBasede)

        }


    }
}
