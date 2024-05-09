using Demoo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demoo.BLL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public IEmployeerepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
    
        Task<int>Completa();
    }
}
