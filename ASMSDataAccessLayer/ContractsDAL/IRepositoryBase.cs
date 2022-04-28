using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASMSDataAccessLayer.ContractsDAL
{
    public interface IRepositoryBase <T,Id> where T:class,new()
    {
        //null verme sebebi diger ozelliklerinda linq sorgusu istemesin hepsini getirsin diye
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,IOrderedQueryable<T>> orderBy=null,
            string includeEntities= null
            );


        //ilk basta filtreleme yaptık.
        //include entities inner join ile ilişkili oldugu entityi getirecektir.
        //linq sorgusu ile istedigimiz kişiyi özelliği cagırabilecegiz Expressionlarda
        T GetFirstOrDefault(Expression<Func<T, bool>> filter=null, string includeEntities = null);

        T GetById(Id id); //Id'yi bagımsızlastırdım. yukarıdada yazarsak city'im byte

        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);

       
    }
}
