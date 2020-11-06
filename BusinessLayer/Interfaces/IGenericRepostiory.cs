using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IGenericRepostiory<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
