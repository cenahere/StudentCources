using BusinessLayer.Interfaces;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private IGenericRepostiory<T> _entity;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IGenericRepostiory<T> Entity
        {
            get
            {
                //  لو الريبزتري نل تنشا نسخه جديد ه  لو لا تنشا نسخة جديده 
                return _entity ?? (_entity = new GenericRepostiory<T>(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
