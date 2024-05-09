using Demoo.BLL.Interfaces;
using Demoo.BLL.Repositories;
using Demoo.DAL.Context;
using Demoo.BLL.Interfaces;
using Demoo.BLL.Repositories;
using Demoo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demoo.BLL
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly MVCAppDBContext _dbContext;

        public IEmployeerepository EmployeeRepository { get ; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }
      
        public UnitOfWork(MVCAppDBContext dbContext)
        {  _dbContext = dbContext;
            EmployeeRepository = new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepositor(_dbContext);
           
          
        }
        public async Task<int>Completa()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
