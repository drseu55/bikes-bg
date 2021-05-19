using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;
using Microsoft.EntityFrameworkCore;

namespace bikes_bg.Repository.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        private AppDbContext context = null;
        private DbSet<T> table = null;
        

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            
            table = this.context.Set<T>();
        }

        public void Delete(object id)
        {
            T obj = table.Find(id);
            if (obj != null)
            {
                table.Remove(obj);
                context.SaveChanges();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return table;
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
        }

        DbSet<T> IGenericRepository<T>.GetTable()
        {
            return table;
        }
    }
}
