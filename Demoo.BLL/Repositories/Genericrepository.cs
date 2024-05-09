using Demoo.BLL.Interfaces;
using Demoo.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demoo.BLL.Repositories
{
    public class Genericrepository <T> : IGenericrepository<T> where T : class
    {
        private readonly MVCAppDBContext _Dbcontext;

        public Genericrepository(MVCAppDBContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }



        public int Add(T item)
        {
            _Dbcontext.Add(item);
            return _Dbcontext.SaveChanges();
        }

        public int Delete(T item)
        {
            _Dbcontext.Remove(item);
            return _Dbcontext.SaveChanges();
        }

        public async Task< IEnumerable<T>> GetAll()
        {
            return await _Dbcontext.Set<T>().ToListAsync();
        }

        public async  Task <T>GetbyId(int id)
        {
            return await _Dbcontext.Set<T>().FindAsync(id);

        }

        public int Update(T item)
        {
            _Dbcontext.Update(item);
            return _Dbcontext.SaveChanges();

        }
    }
    
    
}
