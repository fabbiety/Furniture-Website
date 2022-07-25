using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;

namespace Team.DataBase.Repository
{// Implementing the Repository that uses DbContext to perform operations on the database which  can be used by generic class
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TeamBaseContext _db;
        internal DbSet<T> dbSet;    //To perform internal operations.
        

        public Repository(TeamBaseContext db)
        {
            _db = db;
           

            this.dbSet=db.Set<T>();  //Binding the DbSet
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null, string? includeProperties = null)

        
        { //to  split each properties
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(
                    new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty); 
                }
            }
            if(orderby != null)
            {
                return orderby(query).ToList();
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query=query.Where(filter);
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }


    }
}
