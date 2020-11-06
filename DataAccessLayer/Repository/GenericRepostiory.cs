using BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repository
{
    public class GenericRepostiory<T> : IGenericRepostiory<T> where T : class
    {
        private readonly DataContext _context;
        public DbSet<T> table = null;

        public GenericRepostiory(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public DataContext Context { get; }

        public void Add(T entity)
        {
            table.Add(entity);
        }

        public void Delete(object id)
        {
            T existing = GetById(id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
         
        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
