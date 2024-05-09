using Demoo.BLL.Interfaces;
using Demoo.DAL;
using Demoo.DAL.Context;
using Demoo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demoo.BLL.Repositories
{
    public class EmployeeRepository : Genericrepository<Employee>, IEmployeerepository
    {
        private readonly MVCAppDBContext _Dbcontext;

        public EmployeeRepository(MVCAppDBContext dbcontext) : base(dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        public IQueryable<Employee> searchByName(string name)
         => _Dbcontext.Employees.Where(E => E.Name.ToLower().Contains(name));
    }
}
