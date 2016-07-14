using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Repository
{
    public interface IRepository<T>
    {
        int Add(T entity);
        void Delete(T entity);        
    }
}
