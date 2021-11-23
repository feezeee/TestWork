using System;
using System.Collections.Generic;

namespace App.DAL.Data.DbSet
{
    public interface IDbSet<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public IEnumerable<T> Find(Func<T, Boolean> predicate);
        public void Create(T item);
        public void Update(T item);
        public void Delete(int id);

    }
}