using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demoo.BLL.Interfaces
{
    public interface IGenericrepository<T>
    {
        Task <IEnumerable<T>> GetAll();
        Task< T> GetbyId(int d);
        int Add(T item); 
        int Update(T item);
        int Delete(T item);
    }
}
