using Demoo.BLL.Interfaces;
using Demoo.DAL.Context;
using Demoo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demoo.BLL.Repositories
{
    public class DepartmentRepositor : Genericrepository<Department> , IDepartmentRepository
    {
        public DepartmentRepositor(MVCAppDBContext dbcontext) : base(dbcontext)
    {

    }
    
       
    }
}
