using Demoo.DAL;
using Demoo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demoo.BLL.Interfaces
{
    public interface IEmployeerepository : IGenericrepository<Employee>
    {
        IQueryable<Employee> searchByName(string name);
      
    }
    //IQueryable<Employee> GetemployeeByaddress(string address);
}

